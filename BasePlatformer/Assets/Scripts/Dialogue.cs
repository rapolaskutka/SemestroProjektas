using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI TextMesh;
    public string[] sentences;
    private int index = 0;
    [SerializeField]
    private float TypingSpeed;
    private bool allowed;
    void Start()
    {
        StartCoroutine(Typing());
    }
    private void Update()
    {
        if (TextMesh.text.CompareTo(sentences[index]) == 0) allowed = true;
        if (Input.GetKeyDown(KeyCode.C) && allowed) NextSentence();
    }
    IEnumerator Typing()
    {
        foreach (var item in sentences[index])
        {
            TextMesh.text += item;
            yield return new WaitForSeconds(TypingSpeed);
        }
    }
    public void NextSentence() 
    {
        allowed = false;
        if (index < sentences.Length - 1)
        {
            index++;
            TextMesh.text = "";
            StartCoroutine(Typing());
        }
        else TextMesh.text = "";
    }
}
