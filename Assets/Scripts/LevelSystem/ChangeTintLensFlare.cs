using System;
using UnityEngine;
using UnityEngine.Rendering;

public class ChangeTintLensFlare : MonoBehaviour
{
    private LensFlareComponentSRP lensflare;

    private void Start()
    {
        lensflare = GetComponent<LensFlareComponentSRP>();
    }
    
    public void ChangeTint(Color newColor)
    {
        if (lensflare != null)
        {
            lensflare.lensFlareData.elements[0].tint = newColor;
        }
    }
}
