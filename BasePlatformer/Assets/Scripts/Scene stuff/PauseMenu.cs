using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool Paused = false;
    public GameObject UI;
    private void Start()
    {
      UI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused) Resume();
            else Pause();
        }
    }
    public void Resume()
    {
        UI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }
    void Pause()
    {
        UI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OptionsMenu()
    {
        SceneManager.LoadScene("Best");
    }
}
