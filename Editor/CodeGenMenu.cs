using System;
using UnityEditor;

namespace YanickSenn.CodeGen.Editor {

    public static class CodeGenMenu {

        [MenuItem("Tools/Code Generation/Generate All")]
        public static void GenerateAll() {
            var generatorTypes = TypeCache.GetTypesDerivedFrom<IGenerator>();
            foreach (var type in generatorTypes) {
                if (type.IsAbstract || type.IsInterface) {
                    continue;
                }

                var generator = (IGenerator) Activator.CreateInstance(type);
                generator.Generate();
            }
        }

        [MenuItem("Tools/Code Generation/Clear All")]
        public static void ClearAll() {
            var generatorTypes = TypeCache.GetTypesDerivedFrom<IGenerator>();
            foreach (var type in generatorTypes) {
                if (type.IsAbstract || type.IsInterface) {
                    continue;
                }

                var generator = (IGenerator) Activator.CreateInstance(type);
                generator.Clear();
            }
        }
    }
}
