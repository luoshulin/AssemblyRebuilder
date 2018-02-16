namespace AssemblyRebuilder
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_AssemblyPath = new System.Windows.Forms.TextBox();
            this.bt_OpenAssembly = new System.Windows.Forms.Button();
            this.ofd_OpenAssembly = new System.Windows.Forms.OpenFileDialog();
            this.cmb_EntryPoint = new System.Windows.Forms.ComboBox();
            this.bt_Rebuild = new System.Windows.Forms.Button();
            this.cmb_ManifestModuleKind = new System.Windows.Forms.ComboBox();
            this.chk_NoStaticConstructor = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tb_AssemblyPath
            // 
            this.tb_AssemblyPath.Location = new System.Drawing.Point(12, 13);
            this.tb_AssemblyPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_AssemblyPath.Name = "tb_AssemblyPath";
            this.tb_AssemblyPath.Size = new System.Drawing.Size(965, 23);
            this.tb_AssemblyPath.TabIndex = 0;
            // 
            // bt_OpenAssembly
            // 
            this.bt_OpenAssembly.Location = new System.Drawing.Point(983, 13);
            this.bt_OpenAssembly.Name = "bt_OpenAssembly";
            this.bt_OpenAssembly.Size = new System.Drawing.Size(98, 23);
            this.bt_OpenAssembly.TabIndex = 1;
            this.bt_OpenAssembly.Text = "选择程序集...";
            this.bt_OpenAssembly.UseVisualStyleBackColor = true;
            this.bt_OpenAssembly.Click += new System.EventHandler(this.bt_OpenAssembly_Click);
            // 
            // cmb_EntryPoint
            // 
            this.cmb_EntryPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_EntryPoint.Enabled = false;
            this.cmb_EntryPoint.FormattingEnabled = true;
            this.cmb_EntryPoint.Location = new System.Drawing.Point(12, 43);
            this.cmb_EntryPoint.Name = "cmb_EntryPoint";
            this.cmb_EntryPoint.Size = new System.Drawing.Size(718, 25);
            this.cmb_EntryPoint.TabIndex = 2;
            // 
            // bt_Rebuild
            // 
            this.bt_Rebuild.Enabled = false;
            this.bt_Rebuild.Location = new System.Drawing.Point(983, 42);
            this.bt_Rebuild.Name = "bt_Rebuild";
            this.bt_Rebuild.Size = new System.Drawing.Size(98, 27);
            this.bt_Rebuild.TabIndex = 3;
            this.bt_Rebuild.Text = "重建";
            this.bt_Rebuild.UseVisualStyleBackColor = true;
            this.bt_Rebuild.Click += new System.EventHandler(this.bt_Rebuild_Click);
            // 
            // cmb_ManifestModuleKind
            // 
            this.cmb_ManifestModuleKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ManifestModuleKind.Enabled = false;
            this.cmb_ManifestModuleKind.FormattingEnabled = true;
            this.cmb_ManifestModuleKind.Location = new System.Drawing.Point(853, 43);
            this.cmb_ManifestModuleKind.Name = "cmb_ManifestModuleKind";
            this.cmb_ManifestModuleKind.Size = new System.Drawing.Size(124, 25);
            this.cmb_ManifestModuleKind.TabIndex = 4;
            // 
            // chk_NoStaticConstructor
            // 
            this.chk_NoStaticConstructor.AutoSize = true;
            this.chk_NoStaticConstructor.Checked = true;
            this.chk_NoStaticConstructor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_NoStaticConstructor.Enabled = false;
            this.chk_NoStaticConstructor.Location = new System.Drawing.Point(736, 45);
            this.chk_NoStaticConstructor.Name = "chk_NoStaticConstructor";
            this.chk_NoStaticConstructor.Size = new System.Drawing.Size(111, 21);
            this.chk_NoStaticConstructor.TabIndex = 5;
            this.chk_NoStaticConstructor.Text = "过滤静态构造器";
            this.chk_NoStaticConstructor.UseVisualStyleBackColor = true;
            this.chk_NoStaticConstructor.CheckedChanged += new System.EventHandler(this.chk_NoStaticConstructor_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 82);
            this.Controls.Add(this.chk_NoStaticConstructor);
            this.Controls.Add(this.cmb_ManifestModuleKind);
            this.Controls.Add(this.bt_Rebuild);
            this.Controls.Add(this.cmb_EntryPoint);
            this.Controls.Add(this.bt_OpenAssembly);
            this.Controls.Add(this.tb_AssemblyPath);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "AssemblyRebuilder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_AssemblyPath;
        private System.Windows.Forms.Button bt_OpenAssembly;
        private System.Windows.Forms.OpenFileDialog ofd_OpenAssembly;
        private System.Windows.Forms.ComboBox cmb_EntryPoint;
        private System.Windows.Forms.Button bt_Rebuild;
        private System.Windows.Forms.ComboBox cmb_ManifestModuleKind;
        private System.Windows.Forms.CheckBox chk_NoStaticConstructor;
    }
}