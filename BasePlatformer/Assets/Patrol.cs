using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    private bool movingRight = true;
    public Transform Ground;
    private int layer_mask;
    private void Start()
    {
        layer_mask = LayerMask.GetMask("Walls");
    }
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D ground = Physics2D.Raycast(Ground.position, Vector2.down, 2f, layer_mask);

        if (ground.collider == false)
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, -0, 0);
                movingRight = true;
            }
    }
}
