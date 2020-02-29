using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awake : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Finish")gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
