using System.IO;
using UnityEditor;
using UnityEngine.UIElements;
public class CustomSettingsProvider : SettingsProvider
{
    private SerializedObject SerializedSettings;

    //---SerializedProperties for each setting---
    SerializedProperty debugLogsProp;
    SerializedProperty warningLogsProp;
    SerializedProperty errorLogsProp;

    //Constructor
    //SettingsScope determines where the settings are stored (User(Preferences) or Project(Project Settings))
    //Project scope is only effects the current project, while User scope is global to the user
    public CustomSettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
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
        var provider = new CustomSettingsProvider($"Project/{CustomSettings.PACKAGENAME}", SettingsScope.Project);
        return provider;
    }
}
