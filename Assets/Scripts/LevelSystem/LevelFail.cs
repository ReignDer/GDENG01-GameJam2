using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFail : MonoBehaviour
{
    [SerializeField] GameObject FailScreen;

    void Start()
    {
        FailScreen.SetActive(false);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            FailScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
