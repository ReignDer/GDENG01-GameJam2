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
    
    bool isWorldSwitched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputReader.Mirror += SwitchWorld;
        
        if(RenderSettings.skybox.HasProperty(TINT_PROPERTY))
            NormalSkyColor = RenderSettings.skybox.GetColor(TINT_PROPERTY);
        else if(RenderSettings.skybox.HasProperty(SKYTINT_PROPERTY))
            NormalSkyColor = RenderSettings.skybox.GetColor(SKYTINT_PROPERTY);
        
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
        if (RenderSettings.skybox.HasProperty(TINT_PROPERTY))
            RenderSettings.skybox.SetColor(TINT_PROPERTY, tintColor);
        else if (RenderSettings.skybox.HasProperty(SKYTINT_PROPERTY))
            RenderSettings.skybox.SetColor(SKYTINT_PROPERTY, tintColor);
    }

}
