using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
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
    private float JumpTimeCounter;
    public float JumpTime;
    private bool Jumping;
    private void Start()
    {
        Jumps = ExtraJumpCount;
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, WhatIsGround);
        MoveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
        if (!facingRight && MoveInput > 0) Flip();
        else if (facingRight && MoveInput < 0) Flip();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("DashMove");
        }
        if (Grounded) Jumps = ExtraJumpCount;
        if (Input.GetKeyDown(KeyCode.UpArrow) && Jumps > 0)
        {
            Jumping = true;
            rb.velocity = Vector2.up * JumpForce;
            Jumps--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && Jumps == 0 && Grounded)
        {
            rb.velocity = Vector2.up * JumpForce;
        }
        if (Input.GetKey(KeyCode.UpArrow) && Jumping)
        {
            if (JumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * JumpForce;
                JumpTimeCounter -= Time.deltaTime;
            }
            else Jumping = false;

        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) { Jumping = false; JumpTimeCounter = JumpTime; }
     
        
    }
    IEnumerator DashMove()
    {
        speed += 30;
        yield return new WaitForSeconds(.3f);
        speed -= 30;
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
