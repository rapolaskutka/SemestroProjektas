using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public float JumpForce;
    private float MoveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool Grounded;
    public Transform GroundCheck;
    public float CheckRadius;
    public LayerMask WhatIsGround;
    private int Jumps;
    public int ExtraJumpCount; 
    private void Start()
    {
        Jumps = ExtraJumpCount;
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(GroundCheck.position,CheckRadius, WhatIsGround);
        MoveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
        if (!facingRight && MoveInput > 0) Flip();
        else if (facingRight && MoveInput < 0) Flip();

    }
    void Update()
    {
        Debug.Log(Grounded);
       if (Grounded) Jumps = ExtraJumpCount;
        if (Input.GetKeyDown(KeyCode.UpArrow) && Jumps > 0)
        {
            rb.velocity = Vector2.up * JumpForce;
            Jumps--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && Jumps == 0 && Grounded)
        {
            rb.velocity = Vector2.up * JumpForce;
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
