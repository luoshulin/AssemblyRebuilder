using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using dnlib.DotNet;

namespace AssemblyRebuilder
{
    internal partial class MainForm : Form
    {
        private static readonly string ProgramName = Assembly.GetExecutingAssembly().GetName().Name;

        public string AssemblyPath { get; set; }

        public ModuleDef ManifestModule { get; set; }

        public IManagedEntryPoint ManagedEntryPoint { get; set; }

        public ModuleKind ManifestModuleKind { get; set; }

        public MainForm()
        {
            InitializeComponent();
            cmbManifestModuleKind.SelectedIndexChanged += (object sender, EventArgs e) => { ManifestModuleKind = (ModuleKind)cmbManifestModuleKind.SelectedItem; LoadAllEntryPoints(); };
            Text = $"{Application.ProductName} v{Application.ProductVersion}";
            tbAssemblyPath.DataBindings.Add("Text", this, "AssemblyPath", true, DataSourceUpdateMode.OnPropertyChanged);
            //cmbEntryPoint.DataBindings.Add("SelectedItem", this, "ManagedEntryPoint", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbEntryPoint.SelectedIndexChanged += (object sender, EventArgs e) => ManagedEntryPoint = ((MethodDefWrapper)cmbEntryPoint.SelectedItem).MethodDefine;
            for (int i = 0; i < 4; i++)
                cmbManifestModuleKind.Items.Add((ModuleKind)i);
            //cmbManifestModuleKind.DataBindings.Add("SelectedItem", this, "ManifestModuleKind", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        public MainForm(string assemblyPath) : this()
        {
            AssemblyPath = Path.GetFullPath(assemblyPath);
            LoadAssembly();
        }

        private void btOpenAssembly_Click(object sender, EventArgs e)
        {
            if (ofdOpenAssembly.ShowDialog() == DialogResult.OK)
                tbAssemblyPath.Text = ofdOpenAssembly.FileName;
            else
                return;
            LoadAssembly();
        }

        private void chkNoStaticConstructor_CheckedChanged(object sender, EventArgs e)
        {
            LoadAllEntryPoints();
        }

        private void btRebuild_Click(object sender, EventArgs e)
        {
            Rebuild();
        }

        private void LoadAssembly()
        {
            try
            {
                ManifestModule = ModuleDefMD.Load(AssemblyPath);
                //cmbEntryPoint.Enabled = true; 被cmbEntryPoint.Enabled = MustHasManagedEntryPoint();替代
                chkNoStaticConstructor.Enabled = true;
                cmbManifestModuleKind.Enabled = true;
                btRebuild.Enabled = true;
            }
            catch
            {
                MessageBox.Show("无效程序集，请重新选定路径", ProgramName);
                ManifestModule = null;
                cmbEntryPoint.Enabled = false;
                chkNoStaticConstructor.Enabled = false;
                cmbManifestModuleKind.Enabled = false;
                btRebuild.Enabled = false;
                return;
            }
            LoadManifestModuleKind();
            LoadAllEntryPoints();
        }

        private void LoadAllEntryPoints()
        {
            MethodSig methodSig;

            cmbEntryPoint.Items.Clear();
            cmbEntryPoint.Enabled = MustHasManagedEntryPoint();
            if (!cmbEntryPoint.Enabled)
                return;
            foreach (TypeDef typeDef in ManifestModule.GetTypes())
                foreach (MethodDef methodDef in typeDef.Methods)
                {
                    if (!methodDef.IsStatic)
                        break;
                    if (methodDef.IsGetter || methodDef.IsSetter)
                        break;
                    if (chkNoStaticConstructor.Checked && methodDef.IsStaticConstructor)
                        break;
                    methodSig = (MethodSig)methodDef.Signature;
                    switch (methodSig.Params.Count)
                    {
                        case 0:
                            break;
                        case 1:
                            if (methodSig.Params[0].FullName == "System.String[]")
                                break;
                            else
                                continue;
                        default:
                            continue;
                    }
                    switch (methodSig.RetType.FullName)
                    {
                        case "System.Void":
                        case "System.Int32":
                            break;
                        default:
                            continue;
                    }
                    cmbEntryPoint.Items.Add((MethodDefWrapper)methodDef);
                }
            ManagedEntryPoint = ManifestModule.ManagedEntryPoint;
            if (ManagedEntryPoint == null)
                MessageBox.Show("检测到无效的入口点，请在下拉列表中重新选择一个入口点！", ProgramName);
            else
                cmbEntryPoint.SelectedItem = ManagedEntryPoint;
        }

        private void LoadManifestModuleKind()
        {
            ManifestModuleKind = ManifestModule.Kind;
            cmbManifestModuleKind.SelectedItem = ManifestModuleKind;
        }

        private bool MustHasManagedEntryPoint()
        {
            return ManifestModuleKind != ModuleKind.Dll && ManifestModuleKind != ModuleKind.NetModule;
        }

        private void Rebuild()
        {
            if (MustHasManagedEntryPoint() && ManagedEntryPoint == null && MessageBox.Show("未选择入口点，是否重建？", ProgramName, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            string extension;
            string newAssemblyPath;

            extension = Path.GetExtension(AssemblyPath);
            newAssemblyPath = AssemblyPath.Substring(0, AssemblyPath.Length - extension.Length);
            newAssemblyPath = $"{newAssemblyPath}_Rebuilded{extension}";
            ManifestModule.ManagedEntryPoint = ManagedEntryPoint;
            ManifestModule.Kind = ManifestModuleKind;
            ManifestModule.Write(newAssemblyPath);
            MessageBox.Show("重建成功", ProgramName);
        }
    }
}
