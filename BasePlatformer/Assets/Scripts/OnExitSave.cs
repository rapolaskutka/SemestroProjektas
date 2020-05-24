using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnExitSave : MonoBehaviour
{
    // Start is called before the first frame update
    private string Directory;
    void Start()
    {
        Directory = Application.dataPath + "/resume/LastSave.txt";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
    {
        Saving();
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

        if (File.Exists(Directory))
        {
            File.Open(Directory, FileMode.CreateNew);
            File.WriteAllText(Directory, json);
        }
        else
        {
            File.WriteAllText(Directory, json);
        }
    }
    private class Save
    {
        public int health, scene;
        public Vector3 position;
    }
}
