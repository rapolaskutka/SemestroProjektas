using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Saves : MonoBehaviour
{
    // Start is called before the first frame update
    private string Directory;
    void Start()
    {
        Directory = Application.dataPath + "/saves/";
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
    private class Save
    {
        public int health, scene;
        public Vector3 position;
    }
}
