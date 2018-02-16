using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using dnlib.DotNet;

namespace AssemblyRebuilder
{
    public partial class MainForm : Form
    {
        private static readonly string ProgramName = Assembly.GetExecutingAssembly().GetName().Name;

        public string AssemblyPath { get; set; }

        public AssemblyDef AssemblyDefine { get; set; }

        public IManagedEntryPoint ManagedEntryPoint { get; set; }

        public ModuleKind ManifestModuleKind { get; set; }

        public MainForm()
        {
            InitializeComponent();
            Text += $" v{Application.ProductVersion}";
            tb_AssemblyPath.DataBindings.Add("Text", this, "AssemblyPath", true, DataSourceUpdateMode.OnPropertyChanged);
            cmb_EntryPoint.DataBindings.Add("SelectedItem", this, "ManagedEntryPoint", true, DataSourceUpdateMode.OnPropertyChanged);
            for (int i = 0; i < 4; i++)
                cmb_ManifestModuleKind.Items.Add((ModuleKind)i);
            cmb_ManifestModuleKind.DataBindings.Add("SelectedItem", this, "ManifestModuleKind", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        public MainForm(string assemblyPath) : this()
        {
            AssemblyPath = Path.GetFullPath(assemblyPath);
            LoadAssembly();
        }

        private void bt_OpenAssembly_Click(object sender, EventArgs e)
        {
            if (ofd_OpenAssembly.ShowDialog() == DialogResult.OK)
                tb_AssemblyPath.Text = ofd_OpenAssembly.FileName;
            else
                return;
            LoadAssembly();
        }

        private void chk_NoStaticConstructor_CheckedChanged(object sender, EventArgs e)
        {
            LoadAllEntryPoints();
        }

        private void bt_Rebuild_Click(object sender, EventArgs e)
        {
            Rebuild();
        }

        private void LoadAssembly()
        {
            try
            {
                AssemblyDefine = AssemblyDef.Load(AssemblyPath);
                cmb_EntryPoint.Enabled = true;
                chk_NoStaticConstructor.Enabled = true;
                cmb_ManifestModuleKind.Enabled = true;
                bt_Rebuild.Enabled = true;
            }
            catch
            {
                MessageBox.Show("无效程序集，请重新选定路径", ProgramName);
                AssemblyDefine = null;
                cmb_EntryPoint.Enabled = false;
                chk_NoStaticConstructor.Enabled = false;
                cmb_ManifestModuleKind.Enabled = false;
                bt_Rebuild.Enabled = false;
                return;
            }
            LoadAllEntryPoints();
            LoadManifestModuleKind();
        }

        private void LoadAllEntryPoints()
        {
            if (cmb_EntryPoint.Enabled == false)
                return;

            MethodSig methodSig;

            cmb_EntryPoint.Items.Clear();
            foreach (TypeDef typeDef in AssemblyDefine.ManifestModule.GetTypes())
                foreach (MethodDef methodDef in typeDef.Methods)
                {
                    if (!methodDef.IsStatic)
                        break;
                    if (methodDef.IsGetter || methodDef.IsSetter)
                        break;
                    if (chk_NoStaticConstructor.Checked && methodDef.IsStaticConstructor)
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
                    cmb_EntryPoint.Items.Add(methodDef);
                }
            ManagedEntryPoint = AssemblyDefine.ManifestModule.ManagedEntryPoint;
            if (ManagedEntryPoint == null)
                MessageBox.Show("检测到无效的入口点，请在下拉列表中重新选择一个入口点！", ProgramName);
            else
                cmb_EntryPoint.SelectedItem = ManagedEntryPoint;
        }

        private void LoadManifestModuleKind()
        {
            ManifestModuleKind = AssemblyDefine.ManifestModule.Kind;
            cmb_ManifestModuleKind.SelectedItem = ManifestModuleKind;
        }

        private void Rebuild()
        {
            if (ManagedEntryPoint == null && MessageBox.Show("未选择入口点，是否重建？", ProgramName, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            string extension;
            string newAssemblyPath;

            extension = Path.GetExtension(AssemblyPath);
            newAssemblyPath = AssemblyPath.Substring(0, AssemblyPath.Length - extension.Length);
            newAssemblyPath = $"{newAssemblyPath}_Rebuilded{extension}";
            AssemblyDefine.ManifestModule.ManagedEntryPoint = ManagedEntryPoint;
            AssemblyDefine.ManifestModule.Kind = ManifestModuleKind;
            AssemblyDefine.Write(newAssemblyPath);
            MessageBox.Show("重建成功", ProgramName);
        }
    }
}
