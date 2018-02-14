using System;
using System.IO;
using System.Windows.Forms;
using dnlib.DotNet;

namespace AssemblyRebuilder
{
    internal static class Program
    {
#if DEBUG
        [STAThread]
        private static void Main()
        {
            MainA(new string[] { "UnpackMe.Demo_se.exe" });
            //MainA(new string[] { "AssemblyRebuilder.exe" });
        }
#endif

#if DEBUG
        private static void MainA(string[] args)
        {
#else
        [STAThread]
        private static void Main(string[] args)
        {
#endif
            GlobalExceptionCatcher.Catch();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args != null && args.Length == 1 && !string.IsNullOrEmpty(args[0]) && File.Exists(args[0]))
                Application.Run(new MainForm(args[0]));
            else
                Application.Run(new MainForm());
        }
    }
}
