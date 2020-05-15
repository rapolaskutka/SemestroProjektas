using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    static string path = @"C:\Users\Public\ignore.txt";

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

    public  void SaveGame()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        using (StreamWriter file = new StreamWriter(path))
        {
            file.WriteLine(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
