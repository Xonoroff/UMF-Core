using System.Collections.Generic;
using System.IO;

namespace EditorModulesGeneratorModule.Entities
{
    internal class FeatureModuleInfo : ModuleInfo
    {
        private const string FeatureFolderName = "Feature";

        public FeatureModuleInfo(string modulePath, string moduleName) : base(modulePath, moduleName)
        {
        }

        public override IEnumerable<string> GetDirectoriesToCreate()
        {
            yield return Path.Combine($"{ModulePath}",$"{SrcFolderName}",$"{FeatureFolderName}");
            yield return Path.Combine($"{ModulePath}",$"{SrcFolderName}",$"{FeatureFolderName}","Services");
            yield return Path.Combine($"{ModulePath}",$"{SrcFolderName}",$"{FeatureFolderName}","Managers");
            yield return Path.Combine($"{ModulePath}",$"{SrcFolderName}",$"{FeatureFolderName}","ViewManagers");
            yield return Path.Combine($"{ModulePath}",$"{SrcFolderName}",$"{FeatureFolderName}","Entities");
            yield return Path.Combine($"{ModulePath}",$"{SrcFolderName}",$"{FeatureFolderName}","Installers");
            yield return Path.Combine($"{ModulePath}",$"{SrcFolderName}",$"{FeatureFolderName}","Views");
        }

        public override string GetAsmdefPath()
        {
            return Path.Combine($"{ModulePath}",$"{SrcFolderName}",$"{FeatureFolderName}",$"{ModuleName}.Feature.asmdef");
        }
        
        public override string GetModuleName()
        {
            return base.GetModuleName() + "." + FeatureFolderName;
        }
    }
}