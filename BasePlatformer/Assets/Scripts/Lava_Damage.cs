using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava_Damage : MonoBehaviour
{
    private GameObject UI;
    public GameObject blood;
    private Transform PlayerPosition;
    private HealthControl healthscript;
    [SerializeField]
    private int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        healthscript = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthControl>();
        if (collision.CompareTag("Player"))
        {
            if (healthscript.GetDamage(damage, false, 3.0))
            {
                Instantiate(blood, PlayerPosition.position, Quaternion.identity);
                Destroy(GameObject.FindGameObjectWithTag("Player"));
            }
        }
    }

}
