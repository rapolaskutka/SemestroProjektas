using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private HealthControl healthscript;
    [SerializeField]
    private int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            healthscript.GetDamage(damage, false, 3.0);
        }
    }

    private void Start()
    {
        healthscript = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthControl>();
    }
}
