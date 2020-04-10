using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGravity : MonoBehaviour
{
    private CharacterMovement movementclass;
    private Rigidbody2D rb;
    private bool isColliding = false;
    private bool top;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        movementclass = GameObject.FindObjectOfType<CharacterMovement>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        if (collision.CompareTag("Player"))
        {
            if (isColliding) return;
            isColliding = true;
            rb.gravityScale *= -1;
            movementclass.JumpForce *= -1;
            Physics.gravity *= -1;
            Rotation();
            StartCoroutine(Reset());
        }

    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.2f);
        isColliding = false;
    }
    public void Rotation()
    {
        if (top == false)
        {
            movementclass.transform.eulerAngles = new Vector3(0, 0, 180f);
            if (!movementclass.facingRight) movementclass.facingRight = true;

        }
        else {
            if (movementclass.facingRight) movementclass.facingRight = false;
            movementclass.transform.eulerAngles = Vector3.zero;
        } 
        movementclass.facingRight = !movementclass.facingRight;
        top = !top;
    }
}
