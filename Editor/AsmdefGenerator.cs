namespace EditorModulesGeneratorModule
{
    public class AsmdefGenerator
    {
        public string GetFileContent(string assemblyName)
        {
            //TODO: Use guids for references
            return @"{
            ""name"": """ + assemblyName + @""",
            ""references"": [
            ""MF.Core"", 
            ""Zenject""
                ],
            ""optionalUnityReferences"": [],
            ""includePlatforms"": [],
            ""excludePlatforms"": [],
            ""allowUnsafeCode"": false,
            ""overrideReferences"": false,
            ""precompiledReferences"": [],
            ""autoReferenced"": true,
            ""defineConstraints"": [],
            ""versionDefines"": []
        }";
        }
    }
}