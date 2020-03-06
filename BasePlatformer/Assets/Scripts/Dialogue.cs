using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private float TypingSpeed;
    private bool allowed;
    private int index = 0;
    public TextMeshProUGUI TextMesh;
    public string[] sentences;
    public GameObject dialog;
    void Start()
    {
        dialog.SetActive(false);
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
        else dialog.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) dialog.SetActive(true);
        StartCoroutine(Typing());

    }
}
