using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_time : MonoBehaviour  
{

    public float speed;
    public float Radius = 0.1f;

    private Vector2 centre;
    private float angle;

    void Start()
    {
        centre.x = transform.position.x - 140;
        centre.y = transform.position.y - 10;
    }

    // Update is called once per frame
    void Update()
    {
        angle += -speed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        transform.position = centre - offset;
        //transform.Translate(new Vector3(speed * -Mathf.Cos(Time.deltaTime/2), speed * Mathf.Cos(Time.deltaTime/2), 0));
    }
}
