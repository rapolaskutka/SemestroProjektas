using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GM gamemaster;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { gamemaster.CheckpointPos = transform.position; }
    }
    void Start() 
    {
        gamemaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
    }
}
