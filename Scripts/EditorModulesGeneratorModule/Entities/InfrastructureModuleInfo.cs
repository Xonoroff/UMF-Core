using System.Collections.Generic;
using System.IO;

namespace EditorModulesGeneratorModule.Entities
{
    internal class InfrastructureModuleInfo : ModuleInfo
    {
        private const string InfrastructureFolderName = "Infrastructure";
        
        public InfrastructureModuleInfo(string modulePath, string moduleName) : base(modulePath, moduleName)
        {
        }

        public override IEnumerable<string> GetDirectoriesToCreate()
        {
            yield return Path.Combine($"{ModulePath}", $"{SrcFolderName}");
            yield return Path.Combine($"{ModulePath}",$"{SrcFolderName}",$"{InfrastructureFolderName}");
            yield return Path.Combine($"{ModulePath}",$"{SrcFolderName}",$"{InfrastructureFolderName}","Entities");
            yield return Path.Combine($"{ModulePath}",$"{SrcFolderName}",$"{InfrastructureFolderName}","Interfaces");
        }

        public override string GetAsmdefPath()
        {
            return Path.Combine($"{ModulePath}",$"{SrcFolderName}",$"{InfrastructureFolderName}",$"{ModuleName}.Infrastructure.asmdef");
        }

        public override string GetModuleName()
        {
            return base.GetModuleName() + "." + InfrastructureFolderName;
        }
    }
}