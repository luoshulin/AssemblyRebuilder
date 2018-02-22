using dnlib.DotNet;

namespace AssemblyRebuilder
{
    internal class MethodDefWrapper
    {
        public MethodDef MethodDefine { get; set; }

        private MethodDefWrapper(MethodDef methodDefine) => MethodDefine = methodDefine;

        public static implicit operator MethodDefWrapper(MethodDef methodDefine) => new MethodDefWrapper(methodDefine);

        public override string ToString()
        {
            string str;

            str = MethodDefine.ToString();
            str = str.Replace("System.Void", "void").Replace("System.Integer", "int");
            return MethodDefine.MDToken.ToString() + " " + str;
        }
    }
}
