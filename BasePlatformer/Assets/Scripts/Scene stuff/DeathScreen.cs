using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private GameObject UI;
    private GameObject blood;
    private Transform PlayerPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(blood, PlayerPosition.position, Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("Player")); 
            UI.SetActive(true);
        }

    }
    private void Awake()
    {
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        blood = (GameObject)Resources.Load("BloodSplash", typeof(GameObject));
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
