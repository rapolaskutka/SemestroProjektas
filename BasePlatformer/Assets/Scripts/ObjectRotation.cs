using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float Radius = 0.1f;
    [SerializeField] private bool StartRight;
    private Vector2 centre;
    private float angle;


    void Start()
    {
        centre.x = 0;
        centre.y = 0;
    }

    void FixedUpdate()
    {
        angle += -speed * Time.deltaTime;
        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        if (StartRight) transform.position = centre - offset;
        else transform.position = centre + offset;

    }
}
