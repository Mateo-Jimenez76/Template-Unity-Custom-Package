using System.ComponentModel;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;
public class CustomSettingsProvider : SettingsProvider
{

    //---SerializedProperties for each setting---
    SerializedProperty debugLogsProp;
    SerializedProperty warningLogsProp;
    SerializedProperty errorLogsProp;

    private SerializedObject SerializedSettings;

    //Constructor

    public CustomSettingsProvider(string path, SettingsScope scope = CustomSettings.SCOPE)
        : base(path, scope) { }


    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        SerializedSettings = CustomSettings.GetSerializedSettings();

        //Link SerializedProperties to actual settings
        debugLogsProp = SerializedSettings.FindProperty("debugLogs");
        warningLogsProp = SerializedSettings.FindProperty("warningLogs");
        errorLogsProp = SerializedSettings.FindProperty("errorLogs");
    }

    //Renders the settings GUI using IMGUI
    public override void OnGUI(string searchContext)
    {
        SerializedSettings.Update();
        //--- Render each setting ---
        EditorGUILayout.PropertyField(debugLogsProp);
        EditorGUILayout.PropertyField(warningLogsProp);
        EditorGUILayout.PropertyField(errorLogsProp);

        //Apply any changes made in the GUI to the actual settings
        SerializedSettings.ApplyModifiedPropertiesWithoutUndo();
    }

    //Tells unity to render the given SettingsProvider to a window
    [SettingsProvider]
    public static SettingsProvider CreateCustomSettingsProvider()
    {
        //The path is what shows up in the Settings window
        var provider = new CustomSettingsProvider($"Project/{CustomSettings.PACKAGENAME}", CustomSettings.SCOPE);
        return provider;
    }
}
