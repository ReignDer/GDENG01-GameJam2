using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Toggle fullScreenTog;
    public List<ResItem> resolutions = new List<ResItem>();
    private int resolutionIndex;
    public TMP_Text resolutionLabel;
    private void Start()
    {
        fullScreenTog.isOn = Screen.fullScreen;
    }

    public void ApplyGraphics()
    {
        Screen.SetResolution(resolutions[resolutionIndex].horizontal, resolutions[resolutionIndex].vertical, fullScreenTog.isOn);
    }

    public void ResLeft()
    {
        resolutionIndex--;
        if (resolutionIndex < 0)
        {
            resolutionIndex = 0;
        }
        UpdateResolutionLabel();
    }

    public void ResRight()
    {
        resolutionIndex++;
        if (resolutionIndex > resolutions.Count - 1 )
        {
            resolutionIndex = resolutions.Count - 1;
        }
        UpdateResolutionLabel();
    }

    void UpdateResolutionLabel()
    {
        resolutionLabel.text = resolutions[resolutionIndex].horizontal.ToString() + " x "
            + resolutions[resolutionIndex].vertical.ToString();
    }

    public void CloseOptions()
    {
        gameObject.SetActive(false);
    }
}

[Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
