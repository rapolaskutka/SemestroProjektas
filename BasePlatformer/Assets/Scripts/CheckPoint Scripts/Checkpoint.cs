using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GM gamemaster;
    private Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        if (collision.CompareTag("Hat")) { gamemaster.CheckpointPos = transform.position; }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        rb.WakeUp();
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.DownArrow)) { 
            gamemaster.CheckpointPos = transform.position;
        }
    }
    void Start() 
    {
        gamemaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
    }
}
