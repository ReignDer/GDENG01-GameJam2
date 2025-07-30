using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    private TMP_Text levelLabel;

    void Start()
    {
        levelLabel = GetComponent<TMP_Text>();
        UpdateLeveUI();
    }
    void UpdateLeveUI()
    {
        if(SceneManager.GetActiveScene().name == "Tutorial Level")
        {
            levelLabel.text = "Tutorial";
        }
        else
        {
            levelLabel.text = "Level: " + (SceneManager.GetActiveScene().buildIndex - 1);
        }
        
    }
}
