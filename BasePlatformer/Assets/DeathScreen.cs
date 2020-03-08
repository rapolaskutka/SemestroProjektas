using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private GameObject UI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { Destroy(GameObject.FindGameObjectWithTag("Player")); UI.SetActive(true); }

    }
    private void Awake()
    {
        UI = GameObject.FindGameObjectWithTag("DScreen");
    }
    private void Start()
    {

        UI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
