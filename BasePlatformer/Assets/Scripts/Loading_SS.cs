using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading_SS : MonoBehaviour
{
    public int health;
    public Vector3 position;
    public static Loading_SS instance;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameObject LoadsGui = GameObject.Find("PauseMenuCanvas").transform.Find("Loads").gameObject;
                LoadsGui.SetActive(false);
            }
    }
    public void Load()
    {
        Debug.Log("Click");
        string file = gameObject.GetComponentInChildren<Text>().text;
        string save = File.ReadAllText(file);
        Save info = JsonUtility.FromJson<Save>(save);
        GameObject.Find("Info").GetComponent<Info>().health = info.health;
        GameObject.Find("Info").GetComponent<Info>().position = info.position;
        SceneManager.LoadScene(info.scene);
        Time.timeScale = 1f;
        Debug.Log(info.scene);
    }
    public class Save
    {
        public int health, scene;
        public Vector3 position;
    }
}
