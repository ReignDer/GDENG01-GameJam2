using UnityEngine;

public class MirrorWorldAbility : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    [SerializeField] private GameObject NormalWorld;
    [SerializeField] private GameObject MirrorWorld;
    
    bool isWorldSwitched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputReader.Mirror += SwitchWorld; 
            
    }

    private void SwitchWorld(bool isKeyPressed)
    {
        if (isKeyPressed)
        {
            if (!isWorldSwitched)
            {
                MirrorWorld.SetActive(true);
                NormalWorld.SetActive(false);
                isWorldSwitched = true;
            }
            else
            {
                NormalWorld.SetActive(true);
                MirrorWorld.SetActive(false);
                isWorldSwitched = false;
            }
        }
    }

}
