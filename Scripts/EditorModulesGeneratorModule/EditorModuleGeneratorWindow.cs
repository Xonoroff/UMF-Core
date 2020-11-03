using System.Collections.Generic;
using System.IO;
using EditorModulesGeneratorModule.Entities;
using UnityEditor;
using UnityEngine;

namespace EditorModulesGeneratorModule
{
    public class EditorModuleGeneratorWindow : EditorWindow
    {
        private readonly string defaultScriptsFolder = Path.Combine("Assets","Scripts");

        private string moduleName;
        
        private AsmdefGenerator asmdefGenerator = new AsmdefGenerator();
        
        private void OnGUI()
        {
            EditorGUILayout.LabelField("WARNING: After OK button click - collapse Unity Editor and Expand it", EditorStyles.boldLabel);
            moduleName = EditorGUILayout.TextField("Enter module name", moduleName);

            if (string.IsNullOrEmpty(moduleName))
            {
                return;
            }
            
            var modulePath = GetModulePath(moduleName);
            var featureModuleInfo = new FeatureModuleInfo(modulePath, moduleName);
            var infrastructureModuleInfo = new InfrastructureModuleInfo(modulePath, moduleName);
            var modules = new ModuleInfo[2] {featureModuleInfo, infrastructureModuleInfo};

            EditorGUILayout.LabelField("Next directories will be generated", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            foreach (var module in modules)
            {
                foreach (var directoryToCreate in module.GetDirectoriesToCreate())
                {
                    EditorGUILayout.LabelField(directoryToCreate);
                }
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.LabelField("Next files will be generated", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            foreach (var fileToCreate in GetAsmDefFilesToCreate(modulePath, moduleName))
            {
                EditorGUILayout.LabelField(fileToCreate);
            }
            EditorGUI.indentLevel--;

            if (GUILayout.Button("Ok"))
            {
                Directory.CreateDirectory(modulePath); //TODO: Think how to use AssetDatabase
                
                foreach (var module in modules)
                {
                    foreach (var directoryToCreate in module.GetDirectoriesToCreate())
                    {
                        Directory.CreateDirectory(directoryToCreate);
                    }

                    var asmDefPath = module.GetAsmdefPath();
                    using (var asmDefFile = File.CreateText(asmDefPath))
                    {
                        asmDefFile.Write(asmdefGenerator.GetFileContent(module.GetModuleName()));
                    }
                }
                
                //TODO: Add repaint of project window
            }
        }

        private string GetModulePath(string moduleName)
        {
            return Path.Combine(defaultScriptsFolder, moduleName);
        }

        private IEnumerable<string> GetAsmDefFilesToCreate(string modulePath, string moduleName)
        {
            var infrastructureModuleInfo = new InfrastructureModuleInfo(modulePath, moduleName);
            var featureModuleInfo = new FeatureModuleInfo(modulePath, moduleName);
            yield return infrastructureModuleInfo.GetAsmdefPath();
            yield return featureModuleInfo.GetAsmdefPath();
        }
    }
}