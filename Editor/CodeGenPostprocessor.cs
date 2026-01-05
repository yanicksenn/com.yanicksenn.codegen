using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace YanickSenn.CodeGen.Editor {
    public class CodeGenPostprocessor : AssetPostprocessor {
        private static readonly HashSet<IGenerator> Generators = new();

        static CodeGenPostprocessor() {
            var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttribute<GeneratorAttribute>() != null);

            foreach (var type in handlerTypes) {
                if (!typeof(IGenerator).IsAssignableFrom(type)) {
                    Debug.LogError($"Type {type.Name} has GeneratorAttribute but does not implement IGenerator");
                    continue;
                }

                try {
                    var generator = (IGenerator)Activator.CreateInstance(type);
                    Generators.Add(generator);
                } catch (Exception e) {
                    Debug.LogError($"Failed to create instance of violation handler {type.Name}: {e.Message}");
                }
            }
        }

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
            if (CodeGenConfiguration.GetOrCreateSettings().disableAutoCodeGeneration) {
                return;
            }

            foreach (var generator in Generators) {
                if (ShouldRegenerate(importedAssets, deletedAssets, movedAssets, generator)) {
                    generator.Clear();
                    generator.Generate();
                }
            }
        }

        private static bool ShouldRegenerate(string[] importedAssets, string[] deletedAssets, string[] movedAssets, IGenerator generator) {
            return importedAssets.Any(generator.ShouldRegenerateForAsset)
               || deletedAssets.Any(generator.ShouldRegenerateForAsset)
               || movedAssets.Any(generator.ShouldRegenerateForAsset);
        }
    }
}
