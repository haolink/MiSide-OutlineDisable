using System;
using UnityEngine;
using HarmonyLib;
using UnityEngine.Rendering.PostProcessing;
using VertexFragment;

public class DestroySobelOutline
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SobelOutlineRenderer), "Render", new Type[] { typeof(PostProcessRenderContext) })]
    static bool PrefixRender(SobelOutlineRenderer __instance, PostProcessRenderContext context)
    {
        if (__instance.settings.thickness > 0.0f)
        {
            Debug.Log("Weakening post processing effect.");
            __instance.settings.thickness = new FloatParameter() { value = 0.0f };
            __instance.settings.color = new ColorParameter() { value = new ColorParameter() };
        }

        return true;
    }
}

