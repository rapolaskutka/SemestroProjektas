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
    [SerializeField]
    private bool auto;
    private bool allowed;
    private int index = 0;
    public TextMeshProUGUI TextMesh;
    public string[] sentences;
    public GameObject DialogCanvas;

    void Start()
    {
        DialogCanvas.SetActive(false);
    }
    private void Update()
    {
        
        if (auto && allowed) NextSentence(); 
        if (Input.GetKeyDown(KeyCode.C) && allowed) NextSentence();
    }
    IEnumerator Typing()
    {
        foreach (var item in sentences[index])
        {
            TextMesh.text += item;
            yield return new WaitForSeconds(TypingSpeed);
        }
            StartCoroutine(Pause());
    }
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1f);
        allowed = true;
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
        else if (SceneManager.GetActiveScene().name == "Intro") SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Ignore this line
        else
        {
            TextMesh.text = "";
            DialogCanvas.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !DialogCanvas.activeSelf)
        {
            DialogCanvas.SetActive(true);
            StartCoroutine(Typing());
        }
    }
}
