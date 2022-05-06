using UnityEditor;

namespace EditorModulesGeneratorModule
{
    public static class EditorModuleGenerator
    {
        [MenuItem("Assets/Create Module")]
        static void GenerateModule()
        {
            EditorWindow.GetWindow(typeof(EditorModuleGeneratorWindow));
        }
    }
}
