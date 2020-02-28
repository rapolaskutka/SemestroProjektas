using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.velocity = (GameObject.Find("Character").transform.position - transform.position).normalized * speed;
        if (collision.name == "Character") Destroy(gameObject);
    }
}
