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
        if (!System.IO.Directory.Exists(Directory))
        {
            if (File.Exists(Directory + "LastSave.txt"))
            {
                if (File.ReadAllText(Directory + "LastSave.txt").Length != 0)
                    gameObject.transform.Find("Resume").gameObject.SetActive(true);
            }
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
        File.Delete(file);
        SceneManager.LoadScene(info.scene);
        Time.timeScale = 1f;
    }
    public class Save
    {
        public int health, scene;
        public Vector3 position;
    }
}
