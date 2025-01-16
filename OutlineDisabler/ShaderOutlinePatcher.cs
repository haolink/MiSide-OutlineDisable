using System;
using UnityEngine;
using HarmonyLib;
    
public class ShaderOutlinePatcher
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Material), "SetFloat", new Type[] { typeof(string), typeof(float) })]
    static bool PrefixMaterialSetFloat(Material __instance, ref string name, ref float value)
    {
        if ((SurveilRenderers.MainSettings.DisableShaderOutlines && name == "_OutlineWidth") ||
            (SurveilRenderers.MainSettings.DisablePostProcessingOutlines && name == "_OutlineThickness"))
        {
            value = 0.0f;
        }

        return true;
    }
}

