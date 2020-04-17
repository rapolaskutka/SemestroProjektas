using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    private HealthControl healthscript;
    [SerializeField] private int damage;
    [SerializeField] private int cooldown;
    [SerializeField] private bool OneShotDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            healthscript.GetDamage(damage, false, cooldown);
        }
    }

    private void Start()
    {
        healthscript = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthControl>();
        if (OneShotDeath) 
        {
            damage = healthscript.health;
        }
    }
}
