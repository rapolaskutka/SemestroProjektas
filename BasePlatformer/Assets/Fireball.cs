using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject blood;
    [SerializeField]
    private int damage;
    private HealthControl healthscript;
    private Transform Playerpostion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        healthscript = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthControl>();
        Playerpostion = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (healthscript.GetDamage(damage, false, 3.0))
            {
                Instantiate(blood, Playerpostion.position, Quaternion.identity);
                Destroy(GameObject.FindGameObjectWithTag("Player"));
            }

        }
        if (collision.CompareTag("Ground")) Destroy(gameObject); 
    }
}
