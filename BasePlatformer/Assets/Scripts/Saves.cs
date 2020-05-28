using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Saves : MonoBehaviour
{
    // Start is called before the first frame update
    private string Directory;
    private bool bum;
    private GameObject save_text;
    void Start()
    {
        Directory = Application.dataPath + "/saves/";
    }

    // Update is called once per frame
    public void Saving()
    {
        int healths = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthControl>().health;
        Vector3 positions = GameObject.FindGameObjectWithTag("Player").transform.position;
        int scene_index = SceneManager.GetActiveScene().buildIndex;
        string scene_name = SceneManager.GetActiveScene().name;

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
        if(GameObject.Find("SaveText") == null)
        UI_Appear(scene_name);
    }
    private void UI_Appear(string scene_name)
    {
        GameObject save = GameObject.Find("Save");
        GameObject save_logo = Instantiate(save, save.transform);
        save_logo.name = "SaveText";
        save_logo.transform.SetParent(GameObject.Find("Menu").transform);
        RectTransform looking = save.gameObject.GetComponent<RectTransform>();
        save_logo.gameObject.GetComponent<RectTransform>().localScale = looking.localScale;
        save_logo.gameObject.GetComponent<RectTransform>().localPosition = looking.localPosition;
        save_logo.gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, looking.rect.width);
        save_logo.gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, looking.rect.height);
        save_logo.gameObject.GetComponent<RectTransform>().anchorMin = looking.anchorMin;
        save_logo.gameObject.GetComponent<RectTransform>().anchorMax = looking.anchorMax;
        save_logo.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(looking.anchoredPosition.x + 300, looking.anchoredPosition.y);
        save_logo.GetComponentInChildren<TextMeshProUGUI>().text = "Saved on " + scene_name;
        save_logo.GetComponentInChildren<TextMeshProUGUI>().fontSize = 50;
        save_logo.GetComponent<UnityEngine.UI.Button>().enabled = false;
        PauseMenu.GUI_Extra.Add_Gui(save_logo);
        StartCoroutine(Remove(save_logo));
    }
    IEnumerator Remove(GameObject obj)
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(obj);
    }
    private class Save
    {
        public int health, scene;
        public Vector3 position;
    }
}
