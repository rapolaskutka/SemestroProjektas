using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GM gamemaster;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.DownArrow)) { 
            gamemaster.StartingPos = transform.position;
        }
    }
    void Start() 
    {
        gamemaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
    }
}
