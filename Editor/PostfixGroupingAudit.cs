#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UMF.Core.EditorTools
{
    public static class PostfixGroupingAudit
    {
        private static readonly (string Postfix, string ImplFolder, string InfraFolder)[] Rules = new (string, string, string)[]
        {
            // Commands: executors first (more specific), then commands
            ("CommandExecutor",    @"Implementation\\Commands\\Executors", @"Infrastructure\\Commands"),
            ("Command",            @"Implementation\\Commands", @"Infrastructure\\Commands"),

            ("SerializerProvider", @"Implementation\\Providers", @"Infrastructure\\Providers"),
            ("Factory",            @"Implementation\\Factories", @"Infrastructure\\Factories"),
            ("Repository",         @"Implementation\\Repositories", @"Infrastructure\\Repositories"),
            ("Provider",           @"Implementation\\Providers", @"Infrastructure\\Providers"),
            ("Serializer",         @"Implementation\\Serializers", @"Infrastructure\\Serializers"),
            ("Manager",            @"Implementation\\Managers", @"Infrastructure\\Managers"),
            ("Controller",         @"Implementation\\Controllers", @"Infrastructure\\Controllers"),
            ("Service",            @"Implementation\\Services", @"Infrastructure\\Services"),
            ("Extensions",         @"Implementation\\Extensions", @"Infrastructure\\Extensions"),
        };

        [MenuItem("UMF/Tools/Audit Postfix Grouping")] 
        public static void Audit()
        {
            var root = Path.Combine(Application.dataPath, "UMF", "Core", "Runtime", "src");
            if (!Directory.Exists(root))
            {
                Debug.LogWarning($"[UMF] Core src folder not found: {root}");
                return;
            }

            int checkedFiles = 0;
            int outOfPlace = 0;
            foreach (var file in Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories))
            {
                // Skip generated/editor files within Runtime if any
                var fileName = Path.GetFileName(file);
                if (string.Equals(fileName, "AssemblyInfo.cs", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var typeName = Path.GetFileNameWithoutExtension(file);
                var isInterface = typeName.Length > 1 && typeName[0] == 'I' && char.IsUpper(typeName[1]);
                // Allow base/abstract patterns under Infrastructure even if they match an implementation postfix
                // Name-based heuristic: Base* or *Base or SignalBase*
                var isBaseByName = typeName.StartsWith("Base", StringComparison.Ordinal)
                                   || typeName.EndsWith("Base", StringComparison.Ordinal)
                                   || typeName.StartsWith("SignalBase", StringComparison.Ordinal);

                var rule = Rules.FirstOrDefault(r => typeName.EndsWith(r.Postfix, StringComparison.Ordinal));
                if (rule.Postfix == null)
                {
                    continue; // not governed by postfix rules
                }

                checkedFiles++;
                var useInfra = isInterface || isBaseByName;
                var expectedRel = Path.Combine(useInfra ? rule.InfraFolder : rule.ImplFolder);
                var expectedAbs = Path.Combine(root, expectedRel) + Path.DirectorySeparatorChar; // ensure trailing slash

                var normalizedFile = file.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
                var normalizedExpected = expectedAbs.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);

                if (!normalizedFile.StartsWith(normalizedExpected, StringComparison.OrdinalIgnoreCase))
                {
                    outOfPlace++;
                    var assetPath = "Assets" + normalizedFile.Substring(Application.dataPath.Length);
                    var expectedAssetPath = "Assets" + normalizedExpected.Substring(Application.dataPath.Length) + typeName + ".cs";
                    Debug.LogWarning($"[UMF] Out of place: {assetPath}\n  -> expected under: {Path.GetDirectoryName(expectedAssetPath)}");
                }
            }

            if (checkedFiles == 0)
            {
                Debug.Log("[UMF] No postfix-governed files found to audit.");
            }
            else if (outOfPlace == 0)
            {
                Debug.Log($"[UMF] Postfix grouping OK. Checked {checkedFiles} files; 0 out of place.");
            }
            else
            {
                Debug.LogWarning($"[UMF] Postfix grouping audit found {outOfPlace} out-of-place files out of {checkedFiles} checked. See warnings above.");
            }
        }
    }
}
#endif
