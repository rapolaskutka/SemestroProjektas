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
            Destroy(GameObject.Find("Info"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
