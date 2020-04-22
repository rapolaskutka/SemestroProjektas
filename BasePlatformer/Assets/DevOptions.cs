using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevOptions : MonoBehaviour
{
    private HealthControl healthscript;
    private void Start()
    {
        healthscript = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthControl>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (Input.GetKeyDown(KeyCode.F2)) healthscript.AddHealth(true);
    }
}
