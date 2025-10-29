#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UMF.Core.EditorTools
{
    /// <summary>
    /// Moves runtime source files under Assets/UMF/Core/Runtime/src into their proper folders
    /// based on postfix grouping rules defined in CODE_CONVENTIONS.md.
    /// Uses AssetDatabase to preserve .meta GUIDs.
    /// </summary>
    public static class PostfixGroupingMover
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

        [MenuItem("UMF/Tools/Apply Postfix Grouping (Move Files)")]
        public static void MoveFiles()
        {
            var appData = Application.dataPath;
            var root = Path.Combine(appData, "UMF", "Core", "Runtime", "src");
            if (!Directory.Exists(root))
            {
                Debug.LogWarning($"[UMF] Core src folder not found: {root}");
                return;
            }

            int total = 0;
            int moved = 0;
            int skipped = 0;
            int errors = 0;

            try
            {
                AssetDatabase.StartAssetEditing();

                foreach (var file in Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories))
                {
                    var fileName = Path.GetFileName(file);
                    if (string.Equals(fileName, "AssemblyInfo.cs", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    total++;
                    var typeName = Path.GetFileNameWithoutExtension(file);
                    var isInterface = typeName.Length > 1 && typeName[0] == 'I' && char.IsUpper(typeName[1]);
                    var isBaseByName = typeName.StartsWith("Base", StringComparison.Ordinal)
                                       || typeName.EndsWith("Base", StringComparison.Ordinal)
                                       || typeName.StartsWith("SignalBase", StringComparison.Ordinal);

                    var rule = Rules.FirstOrDefault(r => typeName.EndsWith(r.Postfix, StringComparison.Ordinal));
                    if (rule.Postfix == null)
                    {
                        skipped++;
                        continue; // not governed by postfix rules
                    }

                    var useInfra = isInterface || isBaseByName;
                    var expectedRel = Path.Combine(useInfra ? rule.InfraFolder : rule.ImplFolder, typeName + ".cs");
                    var expectedAbs = Path.Combine(root, expectedRel);

                    // Normalize separators
                    var normalizedFile = file.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
                    var normalizedExpected = expectedAbs.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);

                    if (string.Equals(normalizedFile, normalizedExpected, StringComparison.OrdinalIgnoreCase))
                    {
                        skipped++;
                        continue; // already in place
                    }

                    // Ensure destination directory exists (via AssetDatabase to keep Unity DB consistent)
                    var expectedDir = Path.GetDirectoryName(expectedRel) ?? string.Empty;
                    EnsureFoldersExistUnder(root, expectedDir);

                    var assetPath = ToAssetPath(normalizedFile);
                    var destAssetPath = ToAssetPath(normalizedExpected);

                    var result = AssetDatabase.MoveAsset(assetPath, destAssetPath);
                    if (string.IsNullOrEmpty(result))
                    {
                        moved++;
                        Debug.Log($"[UMF] Moved: {assetPath} -> {destAssetPath}");
                    }
                    else
                    {
                        errors++;
                        Debug.LogError($"[UMF] Failed to move {assetPath} -> {destAssetPath}: {result}");
                    }
                }
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
                AssetDatabase.Refresh();
            }

            Debug.Log($"[UMF] Apply Postfix Grouping completed. Total: {total}, Moved: {moved}, Skipped: {skipped}, Errors: {errors}");
        }

        private static void EnsureFoldersExistUnder(string rootAbsPath, string relativePath)
        {
            // relativePath uses OS separators; we need to create folders from Assets/UMF/... root in AssetDatabase terms
            var parts = relativePath
                .Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar)
                .Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                return;
            }

            // Convert absolute root to asset path
            var rootAssetPath = ToAssetPath(rootAbsPath);
            var current = rootAssetPath.TrimEnd('/');

            foreach (var part in parts)
            {
                var next = current + "/" + part;
                if (!AssetDatabase.IsValidFolder(next))
                {
                    var parent = current;
                    var newFolderName = part;
                    var guid = AssetDatabase.CreateFolder(parent, newFolderName);
                    if (string.IsNullOrEmpty(guid))
                    {
                        Debug.LogError($"[UMF] Failed to create folder '{next}' under '{parent}'.");
                        return;
                    }
                }
                current = next;
            }
        }

        private static string ToAssetPath(string absolutePath)
        {
            var dataPath = Application.dataPath.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
            var normalized = absolutePath.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
            if (!normalized.StartsWith(dataPath, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Path is outside Assets folder: {absolutePath}");
            }

            var relative = normalized.Substring(dataPath.Length).TrimStart(Path.DirectorySeparatorChar);
            return "Assets/" + relative.Replace(Path.DirectorySeparatorChar, '/');
        }
    }
}
#endif
