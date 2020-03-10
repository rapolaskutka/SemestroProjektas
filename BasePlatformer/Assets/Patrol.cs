using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    private bool movingRight = true;
    public Transform Ground;
    [SerializeField]
    private LayerMask Hitswhat;
    public GameObject deathparticles;
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D ground = Physics2D.Raycast(Ground.position, Vector2.down, 2f, Hitswhat);
        RaycastHit2D personright = Physics2D.Raycast(Ground.position, Vector2.right, 0.3f, Hitswhat);
        RaycastHit2D personleft = Physics2D.Raycast(Ground.position, Vector2.left, 0.3f, Hitswhat);
        if (ground.collider == false) Collision();
        if (personright.collider  == true || personleft.collider == true) Collision(); 
    }

    private void Collision()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hat"))
        {
            Instantiate(deathparticles, transform.position, Quaternion.identity);
        }
    }
}
