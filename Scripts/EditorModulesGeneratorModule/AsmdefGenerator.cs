namespace EditorModulesGeneratorModule
{
    public class AsmdefGenerator
    {
        public string GetFileContent(string assemblyName)
        {
            return @"{
            ""name"": """ + assemblyName + @""",
            ""references"": [
            ""Core"",
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