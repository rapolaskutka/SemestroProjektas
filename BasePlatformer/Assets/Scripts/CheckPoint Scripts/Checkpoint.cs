using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GM gamemaster;
    [SerializeField] private GameObject particle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gamemaster.StartingPos = transform.position;
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
    void Start() 
    {
        gamemaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
    }
}
