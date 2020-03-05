using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGravity : MonoBehaviour
{
    private CharacterMovement movementclass;

    private Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        movementclass = GameObject.FindObjectOfType<CharacterMovement>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        rb.gravityScale *= -1;
        movementclass.JumpForce *= -1;
        movementclass.Rotation(); 
    }
    IEnumerable ChangeJF()
    {
        yield return new WaitForSeconds(.1f);
        movementclass.JumpForce *= -1;
    }
}
