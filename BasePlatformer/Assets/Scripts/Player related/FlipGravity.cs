using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGravity : MonoBehaviour
{
    private CharacterMovement movementclass;
    private Rigidbody2D rb;
    private bool isColliding = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FlipG();
            movementclass.Jumps = 2;
            rb.velocity = Vector2.zero;
            if (isColliding) return;
            isColliding = true;
            Physics.gravity *= -1;
            Rotation();
            StartCoroutine(Reset());
        }
    }
    public void FlipG() 
    {
        movementclass = GameObject.FindObjectOfType<CharacterMovement>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        rb.gravityScale *= -1;
        movementclass.JumpForce *= -1;
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.2f);
        isColliding = false;
    }
    public void Rotation()
    {
        if (movementclass.top == false)
        {
            movementclass.transform.eulerAngles = new Vector3(0, 0, 180f);
            if (!movementclass.facingRight) movementclass.facingRight = true;

        }
        else {
            if (movementclass.facingRight) movementclass.facingRight = false;
            movementclass.transform.eulerAngles = Vector3.zero;
        } 
        movementclass.facingRight = !movementclass.facingRight;
        movementclass.top = !movementclass.top;
        
    }
}
