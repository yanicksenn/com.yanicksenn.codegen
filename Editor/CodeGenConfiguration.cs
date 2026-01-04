using UnityEditor;
using UnityEngine;
using YanickSenn.Utils.Editor;

namespace YanickSenn.CodeGen.Editor.Editor
{
    [CreateAssetMenu(fileName = "CodeGenConfiguration", menuName = "CodeGen/CodeGenConfiguration")]
    public class CodeGenConfiguration : ScriptableObject
    {
        public const string SettingsPath = "Assets/Settings/CodeGenConfiguration.asset";

        [Header("Code Generation Settings")]
        [Tooltip("Whether code should automatically be regenerated when assets change.")]
        public bool disableAutoCodeGeneration;

        public static CodeGenConfiguration GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<CodeGenConfiguration>(SettingsPath);
            if (settings == null)
            {
                FileUtils.CreateDirectoryIfNeeded("Assets/Settings");
                
                settings = CreateInstance<CodeGenConfiguration>();
                settings.disableAutoCodeGeneration = false;
                
                AssetDatabase.CreateAsset(settings, SettingsPath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
    }
}