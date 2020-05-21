using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class CreateSave : MonoBehaviour
{
    // Start is called before the first frame update
    private string Directory;
    void Start()
    {
        Directory = Application.dataPath + "/saves/";
    }
    private void Awake()
    {
        Save save = new Save
        {
            health = 3,
        };
        string test = JsonUtility.ToJson(save);
        Save saved = JsonUtility.FromJson<Save>(test);
    }
    public void Saving()
    {
        int healths = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthControl>().health;
        Vector3 positions = GameObject.FindGameObjectWithTag("Player").transform.position;
        int scene_index = SceneManager.GetActiveScene().buildIndex;

        Save save = new Save
        {
            health = healths,
            scene = scene_index,
            position = positions,
        };
        string json = JsonUtility.ToJson(save);

        if (File.Exists(Directory + "/save" + scene_index + ".txt"))
        {
            File.Delete(Directory + scene_index + ".txt");
            File.WriteAllText(Directory + scene_index + ".txt", json);
        }
        else
        {
            File.WriteAllText(Directory + scene_index + ".txt", json);
        }
        Debug.Log(json);
    }
    public void LoadDir()
    {
        DirectoryInfo directory = new DirectoryInfo(Directory);
        FileInfo[] files = directory.GetFiles("*.txt");
        GameObject SavesGui = GameObject.Find("Saves");
        GameObject LoadsGui = GameObject.Find("PauseMenuCanvas").transform.Find("Loads").gameObject;
        GameObject menu = GameObject.Find("Menu");
        LoadsGui.SetActive(true);
        menu.SetActive(false);
        foreach (FileInfo file in files)
        {
            GameObject save = Instantiate(SavesGui);
            
        }
    }
    public static GameObject FindObject(this GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
    public void Load(string file)
    {
        string save = File.ReadAllText(file);
        Save info = JsonUtility.FromJson<Save>(save);
        SceneManager.LoadScene(info.scene);
        Load(info);
    }
    public void Load(Save info)
    {
        StartCoroutine(GiveStuff(info.health, info.position));
    }
    IEnumerator GiveStuff(int health, Vector3 position)
    {
        yield return new WaitForSeconds(2);
        GameObject.FindGameObjectWithTag("Player").GetComponent<HealthControl>().health = health;
        GameObject.FindGameObjectWithTag("Player").transform.position = position;
    }
    // Update is called once per frame
    void Update()
    {
    
    }

    public class Save
    {
        public int health, scene;
        public Vector3 position;
    }
}
