using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using YanickSenn.Utils.Editor;
using YanickSenn.Utils.Events;
using YanickSenn.Utils.VContainer;

namespace YanickSenn.CodeGen.Editor {
    public static class EventRegistryLinker {

        [DidReloadScripts]
        private static void Link() {
            var outputDir = "Assets/Generated/Registries/Event";
            if (!System.IO.Directory.Exists(outputDir)) {
                return;
            }

            LinkRegistry(outputDir);
        }

        [MenuItem("Tools/Code Generation/Force Link Event Registry")]
        public static void ForceLink() {
            Link();
        }

        private static void LinkRegistry(string outputDir) {
            var className = "EventRegistry";
            var assetPath = $"{outputDir}/{className}.asset";

            // 1. Find or Create the Registry Asset
            var registry = AssetDatabase.LoadAssetAtPath<ScriptableObjectInstaller>(assetPath);
            if (registry == null) {
                // We need to find the Type of the generated class.
                var registryType = GetTypeByName(className);
                if (registryType == null) {
                    // Script might not be compiled yet or deleted.
                    return;
                }

                registry = ScriptableObject.CreateInstance(registryType) as ScriptableObjectInstaller;
                if (registry != null) {
                    AssetDatabase.CreateAsset(registry, assetPath);
                    Debug.Log($"Created event registry asset: {assetPath}");
                }
            }

            if (registry == null) {
                return;
            }

            // 2. Update References
            var serializedObj = new SerializedObject(registry);
            var assets = FindAllEventAssets();
            var changed = false;

            foreach (var asset in assets) {
                var fieldName = InjectionUtils.SanitizeName(asset.name);
                var prop = serializedObj.FindProperty(fieldName);
                if (prop == null) {
                    continue;
                }

                if (prop.objectReferenceValue != asset) {
                    prop.objectReferenceValue = asset;
                    changed = true;
                }
            }

            if (changed) {
                serializedObj.ApplyModifiedProperties();
                EditorUtility.SetDirty(registry);
                AssetDatabase.SaveAssets();
                Debug.Log($"Updated event registry references: {assetPath}");
            }
        }

        private static List<ScriptableObject> FindAllEventAssets() {
            var eventTypes = new List<Type>();
            eventTypes.Add(typeof(GlobalEvent));
            eventTypes.AddRange(TypeCache.GetTypesDerivedFrom(typeof(Event<>))
                .Where(t => !t.IsAbstract && !t.IsGenericType));

            var allAssets = new Dictionary<string, ScriptableObject>();

            foreach (var type in eventTypes) {
                var assets = InjectionUtils.FindAssetsByType(type);
                foreach (var asset in assets) {
                    var path = AssetDatabase.GetAssetPath(asset);
                    if (!allAssets.ContainsKey(path)) {
                        allAssets.Add(path, asset);
                    }
                }
            }

            return allAssets.Values.OrderBy(a => a.name).ToList();
        }

        private static Type GetTypeByName(string name) {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                var type = assembly.GetType(name);
                if (type != null) return type;
            }
            return null;
        }
    }
}
