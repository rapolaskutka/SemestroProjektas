using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
    
    public float speed = 15f;
    private Rigidbody2D rb;
    private Throwing throwclass;
    public GameObject impact;
    void Awake()
    {
        throwclass = GameObject.FindObjectOfType<Throwing>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            throwclass.RemoveCooldown();
        }
        if (collision.CompareTag("Ground"))
        {
            Instantiate(impact, transform.position, Quaternion.identity);
            rb.velocity = (GameObject.Find("Character").transform.position - transform.position).normalized * speed;
        }
        if(collision.tag.Equals("Enemy"))
        {
            Enemy_Health health= collision.GetComponent<Enemy_Health>();
            health.GetDamage(30, true, 5);
        }
       
    }
}
