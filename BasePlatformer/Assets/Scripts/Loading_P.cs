using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Loading_P : MonoBehaviour
{
    // Start is called before the first frame update
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

}
