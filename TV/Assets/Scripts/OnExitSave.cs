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
        Directory = Application.dataPath + "/resume/";
        if(!System.IO.Directory.Exists(Directory))
        {
            System.IO.Directory.CreateDirectory(Directory);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
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
        Saving(json);
    }
    public void Saving(string json)
    {
        File.WriteAllText(Directory + "LastSave.txt", json);
    }
    private class Save
    {
        public int health, scene;
        public Vector3 position;
    }
}
