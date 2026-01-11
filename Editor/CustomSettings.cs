using UnityEngine;
using UnityEditor;
public class CustomSettings : ScriptableObject
{
    // Name of your package (without spaces or special characters)
    public const string PACKAGENAME ="ExamplePackage";

    // "Editor" restricts your ability to use these settings at runtime
    // So "Resources" is a better location if runtime access is needed
    public const string PARENTFOLDER = "Resources";

    // SettingsScope determines where the settings are stored (User(Preferences) or Project(Project Settings))
    // Project scope is only effects the current project, while User scope is global to the user
    public const SettingsScope SCOPE = SettingsScope.Project;

    public const string SETTINGSPATH = $"Assets/{PARENTFOLDER}/{PACKAGENAME}Settings.asset";

    // --- Settings for your package ---
    [SerializeField] private bool debugLogs;
    // Functions rea made for each varible to provide read-only access
    public bool DebugLogsEnabled() => debugLogs;

    [SerializeField] private bool warningLogs;
    public bool WarningLogsEnabled() => warningLogs;

    [SerializeField] private bool errorLogs;
    public bool ErrorLogsEnabled() => errorLogs;

    /// <summary>
    /// Retrieves the existing custom settings asset if it exists; otherwise, creates a new settings asset with default
    /// values and returns it.
    /// </summary>
    /// <remarks>If the settings asset does not exist at the expected path, this method creates the necessary
    /// folder structure and a new settings asset with default values. The method ensures that a valid settings asset is
    /// always returned, simplifying access to configuration data.</remarks>
    /// <returns>A <see cref="CustomSettings"/> instance representing the current settings. If no settings asset exists, a new
    /// one is created and returned.</returns>
    internal static CustomSettings GetOrCreateSettings()
    {
        //Check that there is a valid location to store the settings.asset
        //If not...
        if (!AssetDatabase.IsValidFolder($"Assets/{PARENTFOLDER}"))
        {
            Logger.LogWarning($"Created {PARENTFOLDER} folder for {PACKAGENAME} settings at Assets/{PARENTFOLDER}");
            //...Create the Resources folder
            AssetDatabase.CreateFolder("Assets", PARENTFOLDER);
        }

        //Try to load the settings asset
        var settings = AssetDatabase.LoadAssetAtPath<CustomSettings>(SETTINGSPATH);

        //If the settings asset does not exist...
        if (settings == null)
        {
            //Create an instance of the settings object
            settings = CreateInstance<CustomSettings>();

            //Set default values for settings
            settings.debugLogs = true;
            settings.warningLogs = true;
            settings.errorLogs = true;
            settings.defaultColliderType = ColliderType.Box;
            settings.defaultCollider2DType = Collider2DType.Box;

            //Save the settings object as an asset
            AssetDatabase.CreateAsset(settings, SETTINGSPATH);
            AssetDatabase.SaveAssets();
            Logger.LogWarning($"Created {PACKAGENAME} settings object at: {SETTINGSPATH}");
        }

        return settings;
    }

    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(GetOrCreateSettings());
    }
}
