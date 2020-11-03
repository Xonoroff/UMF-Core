using System.Collections.Generic;
using System.IO;

namespace EditorModulesGeneratorModule.Entities
{
    internal abstract class ModuleInfo
    {
        protected const string SrcFolderName = "src";
        
        protected readonly string ModulePath;
            
        protected readonly string ModuleName;

        public ModuleInfo(string modulePath, string moduleName)
        {
            this.ModulePath = modulePath;
            this.ModuleName = moduleName;
        }
        
        public abstract IEnumerable<string> GetDirectoriesToCreate();

        public abstract string GetAsmdefPath();
        
        public virtual string GetModuleName()
        {
            return ModuleName;
        }
    }
}