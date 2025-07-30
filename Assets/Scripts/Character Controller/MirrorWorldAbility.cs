using UnityEngine;

public class MirrorWorldAbility : MonoBehaviour
{
    private static readonly int TINT_PROPERTY = Shader.PropertyToID("_Tint");
    private static readonly int SKYTINT_PROPERTY = Shader.PropertyToID("_SkyTint");
    [SerializeField] private InputReader inputReader;

    [SerializeField] private GameObject NormalWorld;
    [SerializeField] private GameObject MirrorWorld;
    [SerializeField] private Light NormalLight;
    [SerializeField] private Light MirrorLight;

    private Color NormalSkyColor;
    private Color MirrorSkyColor;
    
    Material OriginalSkybox;
    Material SkyboxInstance;
    
    bool isWorldSwitched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputReader.Mirror += SwitchWorld;
        
        OriginalSkybox = RenderSettings.skybox;
        SkyboxInstance = new Material(OriginalSkybox);
        RenderSettings.skybox = SkyboxInstance;
        
        if(SkyboxInstance.HasProperty(TINT_PROPERTY))
            NormalSkyColor = SkyboxInstance.GetColor(TINT_PROPERTY);
        else if(SkyboxInstance.HasProperty(SKYTINT_PROPERTY))
            NormalSkyColor = SkyboxInstance.GetColor(SKYTINT_PROPERTY);
        
        MirrorSkyColor = new Color(188f/255f, 84f/255f, 84f/255f, 1f);
    }

    private void SwitchWorld(bool isKeyPressed)
    {
        if (!isKeyPressed) return;
        if (!isWorldSwitched)
        {
            MirrorWorld.SetActive(true);
            NormalWorld.SetActive(false);
            RenderSettings.sun = MirrorLight;
            RenderSwitchedWorld(MirrorSkyColor);
            isWorldSwitched = true;
        }
        else
        {
            NormalWorld.SetActive(true);
            MirrorWorld.SetActive(false);
            RenderSettings.sun = NormalLight;
            RenderSwitchedWorld(NormalSkyColor);
            isWorldSwitched = false;
        }
    }

    void RenderSwitchedWorld(Color tintColor)
    {
        if (SkyboxInstance.HasProperty(TINT_PROPERTY))
            SkyboxInstance.SetColor(TINT_PROPERTY, tintColor);
        else if (SkyboxInstance.HasProperty(SKYTINT_PROPERTY))
            SkyboxInstance.SetColor(SKYTINT_PROPERTY, tintColor);
    }

    void OnDestroy()
    {
        inputReader.Mirror -= SwitchWorld;
    }
    
}
