using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dead")
        {
            Scene currentscene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentscene.name);
        }
        if (collision.gameObject.tag == "TP")
        {
            SceneManager.LoadScene("Best");
        }
    }
}
