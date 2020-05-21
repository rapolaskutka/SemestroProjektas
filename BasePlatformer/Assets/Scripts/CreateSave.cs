using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class CreateSave : MonoBehaviour
{
    // Start is called before the first frame update
    private string Directory;
    public readonly int health;
    public readonly Vector3 position;
    private bool OnLoad;
    void Start()
    {
        Directory = Application.dataPath + "/saves/";
    }
    private void Awake()
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
    public void LoadDir()
    {
        DirectoryInfo directory = new DirectoryInfo(Directory);
        FileInfo[] files = directory.GetFiles("*.txt");
        GameObject LoadsGui = GameObject.Find("PauseMenuCanvas").transform.Find("Loads").gameObject;
        GameObject menu = GameObject.Find("Menu");
        LoadsGui.SetActive(true);
        GameObject SavesGui = GameObject.Find("Saves");
        menu.SetActive(false);
        foreach (FileInfo file in files)
        {
            GameObject save = Instantiate(SavesGui);
            save.gameObject.transform.parent = LoadsGui.gameObject.transform;
            RectTransform looking = SavesGui.gameObject.GetComponent<RectTransform>();
            save.gameObject.GetComponent<RectTransform>().localScale = looking.localScale;
            save.gameObject.GetComponent<RectTransform>().localPosition = looking.localPosition;
            save.gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, looking.rect.width);
            save.gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, looking.rect.height);
            save.gameObject.GetComponent<RectTransform>().anchorMin = looking.anchorMin;
            save.gameObject.GetComponent<RectTransform>().anchorMax = looking.anchorMax;
            save.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(looking.anchoredPosition.x, looking.anchoredPosition.y - 100);
            save.GetComponentInChildren<TextMeshProUGUI>().text = "1 Scene Save";
            save.GetComponentInChildren<Text>().text = file.FullName;
        }
        SavesGui.SetActive(false);
    }
    public void Load()
    {
        string file = gameObject.GetComponentInChildren<Text>().text;
        string save = File.ReadAllText(file);
        Save info = JsonUtility.FromJson<Save>(save);
        OnLoad = true;
        //SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log(info.scene);
        SceneManager.LoadScene(info.scene);
    }
 
    private void OnLevelWasLoaded(int level)
    {
        if (Time.timeScale != 1f)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<HealthControl>().health = health;
            GameObject.FindGameObjectWithTag("Player").transform.position = position;
            Time.timeScale = 1f;
        }
    }
    // Update is called once per frame
    void Update()
    {
       Debug.Log("Bum");
       if (Input.GetKeyDown(KeyCode.Escape))
       {
             GameObject LoadsGui = GameObject.Find("PauseMenuCanvas").transform.Find("Loads").gameObject;
             LoadsGui.SetActive(false);
        }
    }

    public class Save
    {
        public int health, scene;
        public Vector3 position;
    }
}
