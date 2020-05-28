using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBinding : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    private GameObject currentKey;

    public Text up, left, down, right, jump;

    void Start()
    {
        keys.Add("Up", KeyCode.W);
        keys.Add("Down", KeyCode.S);
        keys.Add("Right", KeyCode.D);
        keys.Add("Left", KeyCode.A);

        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        right.text = keys["Right"].ToString();
        left.text = keys["Left"].ToString();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keys["Up"]))
        {
            Debug.Log("Up");
        }

        if (Input.GetKeyDown(keys["Down"]))
        {
            Debug.Log("Down");
        }

        if (Input.GetKeyDown(keys["Left"]))
        {
            Debug.Log("Left");
        }

        if (Input.GetKeyDown(keys["Right"]))
        {
            Debug.Log("Right");
        }
    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}
