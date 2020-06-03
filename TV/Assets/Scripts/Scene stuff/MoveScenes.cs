using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScenes : MonoBehaviour
{
    [SerializeField] private int SceneIndexLeave0ForDefault = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") )
        {
            if (SceneIndexLeave0ForDefault == 0)
            {
                Destroy(GameObject.FindGameObjectWithTag("GM"));
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if (SceneIndexLeave0ForDefault > 0)
            {
                Destroy(GameObject.FindGameObjectWithTag("GM"));
                SceneManager.LoadScene(SceneIndexLeave0ForDefault - 1);
            }

        }

        

    }
}
