using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private GameObject UI;
    public GameObject blood;
    private Transform PlayerPosition;
    private HealthControl healthscript;
    [SerializeField]
    private int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(healthscript.GetDamage(damage, false, 3.0))
            {
                Instantiate(blood, PlayerPosition.position, Quaternion.identity);
                Destroy(GameObject.FindGameObjectWithTag("Player"));
            }
        }
    }
    private void Awake()
    {
        UI = GameObject.FindGameObjectWithTag("DScreen");
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        healthscript = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthControl>();
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
