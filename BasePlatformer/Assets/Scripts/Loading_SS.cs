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
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Load()
    {
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
