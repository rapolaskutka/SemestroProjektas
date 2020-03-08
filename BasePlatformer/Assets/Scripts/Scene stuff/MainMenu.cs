using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    private float WaitTime = 1f;
    public void PlayGame()
    {
        StartCoroutine(LoadLevel("Tutorial"));
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OptionsMenu()
    {
        StartCoroutine(LoadLevel("Best"));
    }
    private IEnumerator LoadLevel(string levelname)
    {
        transition.SetTrigger("Transition");
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene(levelname);
    }
}
