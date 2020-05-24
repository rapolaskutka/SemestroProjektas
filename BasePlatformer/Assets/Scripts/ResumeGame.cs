using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ResumeGame : MonoBehaviour
{
    // Start is called before the first frame update
    private string Directory;
    void Start()
    {
        Directory = Application.dataPath + "/resume/";
        if(File.Exists(Directory + "LastSave.txt"))
        {
            gameObject.transform.Find("LoadGame").gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Load()
    {
        string file = Directory + "LastSave.txt";
        string save = File.ReadAllText(file);
        Save info = JsonUtility.FromJson<Save>(save);
        GameObject.Find("Info").GetComponent<Info>().health = info.health;
        GameObject.Find("Info").GetComponent<Info>().position = info.position;
        SceneManager.LoadScene(info.scene);
        Time.timeScale = 1f;
    }
    public class Save
    {
        public int health, scene;
        public Vector3 position;
    }
}
