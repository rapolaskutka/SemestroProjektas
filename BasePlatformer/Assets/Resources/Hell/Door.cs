using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    getItem Item;
    private BoxCollider2D door;
    // Start is called before the first frame update
    void Start()
    {
        Item = GameObject.FindGameObjectWithTag("Player").GetComponent<getItem>();
        door = GameObject.FindGameObjectWithTag("Door").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (Item.Contain_Item("Key") && !door.isTrigger)
        {
            door.isTrigger = true;
            Item.Remove_Item("Key");
        }
    }
}
