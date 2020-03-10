using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private float TypingSpeed;
    private bool allowed;
    private int index = 0;
    public TextMeshProUGUI TextMesh;
    public TextMeshProUGUI ContinueText;
    public string[] sentences;
    public GameObject dialog;
    void Start()
    {
        ContinueText.text = "";
        dialog.SetActive(false);
    }
    private void Update()
    {
        if (TextMesh.text.CompareTo(sentences[index]) == 0) {
            allowed = true;
            ContinueText.text = "Press C to continue";
        }

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
        ContinueText.text = "";
        if (index < sentences.Length - 1)
        {
            index++;
            TextMesh.text = "";
            StartCoroutine(Typing());
        }
        else if (SceneManager.GetActiveScene().name == "FirstLevel") SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); //Ignore this line
        else dialog.SetActive(false); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) dialog.SetActive(true);
        StartCoroutine(Typing());
    }
}
