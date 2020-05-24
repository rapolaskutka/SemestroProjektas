using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu_Load : MonoBehaviour
{
    private string Directory;
    public static Loading_P instance;
    void Start()
    {
        Directory = Application.dataPath + "/saves/";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject LoadsGui = GameObject.Find("PauseMenuCanvas").transform.Find("Loads").gameObject;
            LoadsGui.SetActive(false);
        }
    }
    public void LoadDir()
    {
        DirectoryInfo directory = new DirectoryInfo(Directory);
        FileInfo[] files = directory.GetFiles("*.txt");
        GameObject Gui = GameObject.Find("Canvas").transform.Find("MainLoad").gameObject;
        GameObject menu = GameObject.Find("MainMenu");
        menu.SetActive(false);
        Gui.SetActive(true);
        GameObject LoadsGui = GameObject.Find("MainLoad");
        //LoadsGui.SetActive(false);
        Debug.Log(LoadsGui.activeSelf);
        GameObject SavesGui = GameObject.Find("LoadSave");
        List<GameObject> saves = new List<GameObject>();
        int i = 0;
        foreach (FileInfo file in files)
        {
            GameObject save = Instantiate(SavesGui, LoadsGui.transform);
            saves.Add(save);
            save.gameObject.transform.SetParent(LoadsGui.transform, true);
            RectTransform looking = SavesGui.gameObject.GetComponent<RectTransform>();
            save.gameObject.GetComponent<RectTransform>().localScale = looking.localScale;
            save.gameObject.GetComponent<RectTransform>().localPosition = looking.localPosition;
            save.gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, looking.rect.width);
            save.gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, looking.rect.height);
            save.gameObject.GetComponent<RectTransform>().anchorMin = looking.anchorMin;
            save.gameObject.GetComponent<RectTransform>().anchorMax = looking.anchorMax;
            save.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(looking.anchoredPosition.x, looking.anchoredPosition.y - i * 200 + 200);
            string name1 = file.Name.Replace(".txt", "");
            int index = int.Parse(name1);
            Debug.Log(SceneManager.GetSceneByBuildIndex(index).name);
            string path = SceneUtility.GetScenePathByBuildIndex(index);
            string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
            save.GetComponentInChildren<TextMeshProUGUI>().text = "Load stage:" + sceneName;
            save.GetComponentInChildren<Text>().text = file.FullName;
            i++;
        }
        SavesGui.SetActive(false);
        Debug.Log(i);
    }
}
