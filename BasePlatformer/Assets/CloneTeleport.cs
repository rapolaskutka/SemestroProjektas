using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneTeleport : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D rb;
    private Throwing throwclass;
    public GameObject impact;
    public Transform PlayerPos;
    void Awake()
    {
        throwclass = GameObject.FindObjectOfType<Throwing>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Instantiate(impact, transform.position, Quaternion.identity);
            PlayerPos = this.transform;
        }

    }
}
