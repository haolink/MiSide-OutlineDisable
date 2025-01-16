using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime.Injection;

public static class PluginInfo
{
	public const string PLUGIN_GUID = "OutlineDisable";
	public const string PLUGIN_NAME = "Outline Disable";
	public const string PLUGIN_VERSION = "0.9.0";

	public static PluginLoader Instance;
	public static string AssetsFolder = Paths.PluginPath + "\\" + PluginInfo.PLUGIN_GUID + "\\Assets";
}

[BepInPlugin("org.miside.plugins.outlinedisable", PluginInfo.PLUGIN_NAME, "0.9.0")]
public class PluginLoader : BasePlugin
{
	public ManualLogSource Logger { get; private set; }

	public PluginLoader() {}

	public override void Load()
	{
		Logger = (this as BasePlugin).Log;		
		PluginInfo.Instance = this;

        if (!ClassInjector.IsTypeRegisteredInIl2Cpp(typeof(OutlineDisableWhenVisible)))
        {
            ClassInjector.RegisterTypeInIl2Cpp(typeof(OutlineDisableWhenVisible));
        }

        IL2CPPChainloader.AddUnityComponent(typeof(SurveilRenderers));
	}
}

