using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    [SerializeField]
    private KeyCode RestartInput;
    void Update()
    {
        if (Input.GetKeyDown(RestartInput))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
