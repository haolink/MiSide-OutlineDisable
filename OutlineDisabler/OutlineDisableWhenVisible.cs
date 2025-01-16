using EPOOutline;
using System;
using UnityEngine;

public class OutlineDisableWhenVisible : MonoBehaviour { 
    public void OnBecameVisible()
    {
        DisableOutlines();
    }

    public void Start()
    {
        DisableOutlines();
    }

    /*public void Update()
    {
        DisableOutlines();
    }*/

    public void RunDisable()
    {
        DisableOutlines();
    }

    private void DisableOutlines()
    {
        Renderer rr = this.gameObject.GetComponent<Renderer>();
        if (rr == null)
        {
            return;
        }        

        foreach (Material mat in rr.materials)
        {
            if (SurveilRenderers.MainSettings.DisableShaderOutlines)
            {
                if (mat.HasFloat("_OutlineWidth"))
                {
                    mat.SetFloat("_OutlineWidth", 0.0f);
                }
            }

            if (SurveilRenderers.MainSettings.DisablePostProcessingOutlines)
            {
                if (mat.HasFloat("_OutlineThickness"))
                {
                    mat.SetFloat("_OutlineThickness", 0.0f);
                }
            }
        }
    }
}
