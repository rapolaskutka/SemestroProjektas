using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    private GM gamemaster;
    void Start()
    {
        gamemaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        transform.position = gamemaster.StartingPos;
    }
    [SerializeField] private GameObject particle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            gamemaster.StartingPos = transform.position;
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
