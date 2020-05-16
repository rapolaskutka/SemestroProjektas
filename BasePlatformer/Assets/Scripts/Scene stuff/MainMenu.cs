using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    static string path = @"C:\Users\Public\ignore.txt";

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
    public void LoadGame()
    {
        if (!File.Exists(path))
        {
            Debug.Log("No File Found");
            return;
        }

        using (StreamReader file = new StreamReader(path))
        {
            int value = (int.Parse(file.ReadLine()) - 9127412 ) / 32856875 ;
            if (value > 0 && value < 15)
            { SceneManager.LoadScene(value); }
            else Application.Quit();
               
        }
    }

}
