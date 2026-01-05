using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace YanickSenn.CodeGen.Editor {

    public static class CodeGenMenu {

        [MenuItem("Tools/Code Generation/Regenerate All")]
        public static void RegenerateAll() {
            foreach (var generator in GetAllGenerators()) {
                generator.Clear();
                generator.Generate();
            }
        }

        [MenuItem("Tools/Code Generation/Clear All")]
        public static void ClearAll() {
            foreach (var generator in GetAllGenerators()) {
                generator.Clear();
            }
        }

        private static IEnumerable<IGenerator> GetAllGenerators() {
            return TypeCache.GetTypesDerivedFrom<IGenerator>().Where(type => !type.IsAbstract && !type.IsInterface).Select(type => (IGenerator) Activator.CreateInstance(type));
        }
    }
}
