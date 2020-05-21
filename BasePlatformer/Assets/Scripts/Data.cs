using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        int health = GameObject.Find("Info").GetComponent<Info>().health;
        Vector3 Position = GameObject.Find("Info").GetComponent<Info>().position;
        if(health != -1)
        {
            GameObject.Find("Character").GetComponent<HealthControl>().health = health;
            GameObject.Find("Character").transform.position = Position;
            GameObject.Find("Info").GetComponent<Info>().health = -1;
            GameObject.Find("Info").GetComponent<Info>().position = new Vector3();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
