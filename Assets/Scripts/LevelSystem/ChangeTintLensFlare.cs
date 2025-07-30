using System;
using UnityEngine;
using UnityEngine.Rendering;

public class ChangeTintLensFlare : MonoBehaviour
{
    private LensFlareComponentSRP lensflare;
    LensFlareDataSRP lensflareDataInstance;
    LensFlareDataSRP OriginallensflareData;

    private void Start()
    {
        lensflare = GetComponent<LensFlareComponentSRP>();
        OriginallensflareData = lensflare.lensFlareData;
        lensflareDataInstance = Instantiate(OriginallensflareData);
        lensflare.lensFlareData = lensflareDataInstance;
    }
    
    public void ChangeTint(Color newColor)
    {
        if (lensflare != null)
        {
            lensflareDataInstance.elements[0].tint = newColor;
        }
    }
    
    private void OnDestroy()
    {
        // Clean up the runtime instance when exiting play mode
        if (lensflareDataInstance != null)
        {
            DestroyImmediate(lensflareDataInstance);
        }
    
        // Optionally restore original data (though Unity handles this automatically)
        if (lensflare != null && lensflareDataInstance != null)
        {
            lensflare.lensFlareData = lensflareDataInstance;
        }
    }
}
