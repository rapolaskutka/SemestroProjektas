using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int health;
    GameObject character;
    void Start()
    {
        character = GetComponent("Character").gameObject;
        if(character != null)
        {
            for(int i =0; i < health; i++)
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
