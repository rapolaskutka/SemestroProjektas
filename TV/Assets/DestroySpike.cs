using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpike : MonoBehaviour
{
    getItem Item;
    // Start is called before the first frame update
    void Start()
    {
        Item = GameObject.FindGameObjectWithTag("Player").GetComponent<getItem>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            if (Item.Contain_Item("Key"))
            {

                Item.Remove_Item("Key");
                Destroy(gameObject);
            }
            else
            {
                HealthControl healthscript = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthControl>();
                healthscript.GetDamage(healthscript.health, false, 1f);
            }
        }
    }
}
