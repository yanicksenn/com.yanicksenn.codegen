using System.Collections.Generic;
using UnityEditor;
using YanickSenn.CodeGen.Editor.Editor;

namespace YanickSenn.CodeGen.Editor
{
    static class CodeGenConfigurationProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var provider = new SettingsProvider("Project/YanickSenn/Code Generation", SettingsScope.Project)
            {
                label = "Code Generation",
                guiHandler = (searchContext) =>
                {
                    var settings = CodeGenConfiguration.GetSerializedSettings();
                    settings.Update();
                    
                    EditorGUILayout.PropertyField(settings.FindProperty(nameof(CodeGenConfiguration.disableAutoCodeGeneration)));

                    settings.ApplyModifiedProperties();
                },

                keywords = new HashSet<string>(new[] { "Package", "Generator", "Author", "Namespace" })
            };

            return provider;
        }
    }
}
