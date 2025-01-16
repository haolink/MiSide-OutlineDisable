using UnityEngine;
using HarmonyLib;
using EPOOutline;
using System.Text.Json;
using System.Reflection;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Playables;
using VertexFragment;

public class SurveilRenderers : MonoBehaviour
{     
    public class Settings
    {
        public bool DisableHighlighter { get; set; } = false;
        public bool DisablePostProcessingOutlines { get; set; } = true;
        public bool DisableShaderOutlines { get; set; } = true;
    }

    public static Settings MainSettings = new Settings();

    private void Start()
    {
        string baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string path = Path.Combine(baseDirectory ?? "", "settings.json");

        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);

                Settings rawSettings = JsonSerializer.Deserialize<Settings>(json);

                if (rawSettings != null)
                {
                    MainSettings = rawSettings;
                }
            }
            catch
            {
                MainSettings = new Settings();
            }
        }

        if (MainSettings.DisableShaderOutlines || MainSettings.DisablePostProcessingOutlines)
        {
            Harmony harmony = new Harmony("org.miside.plugins.outlinedisable.collider_enable_watch");
            
            if (MainSettings.DisableShaderOutlines)
            {
                harmony.PatchAll(typeof(ShaderOutlinePatcher));
            }

            if (MainSettings.DisablePostProcessingOutlines)
            {
                harmony.PatchAll(typeof(DestroySobelOutline));
            }
        }
        
    }

    /// <summary>
    /// Hook to Unity's Update method.
    /// </summary>
    void Update()
    {
        // Horribly inefficient, cycles through all renderers on each frame render and updates shader settings...
        // but works as a proof of concept
        Renderer[] renderers = GameObject.FindObjectsOfType<Renderer>();
        foreach (Renderer r in renderers)
        {
            OutlineDisableWhenVisible odwv = r.gameObject.GetComponent<OutlineDisableWhenVisible>();
            if (odwv == null)
            {
                odwv = r.gameObject.AddComponent<OutlineDisableWhenVisible>();
                odwv.enabled = true;
            }
        }
        
        if (MainSettings.DisableHighlighter)
        {
            Outliner[] outliners = GameObject.FindObjectsOfType<Outliner>(false);
            foreach (Outliner outliner in outliners)
            {
                outliner.enabled = false;
            }
        }

        /*if (MainSettings.DisablePostProcessingOutlines)
        {*/
            PostProcessVolume[] volumeProcessors = GameObject.FindObjectsOfType<PostProcessVolume>(true);
            foreach (PostProcessVolume volumeProcessor in volumeProcessors)
            {
                if (volumeProcessor.sharedProfile.GetSetting<SobelOutline>() != null)
                {
                    Debug.Log("Destroying post processing effect.");
                    volumeProcessor.sharedProfile.RemoveSettings<SobelOutline>();
                }
            }
        //}
    }    
  
    void OnApplicationQuit()
    {
        
    }
}
