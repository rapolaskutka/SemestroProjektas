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
    public float WallJumpForce;
    private bool touch;
    public float wallSlideSpeed;
    private bool islide;
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
        ApplySliding();

    }
    public void ApplySliding()
    {
        if(islide)
        {
            if(rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }
    public void CheckIfSliding()
    {
        if(touch && !Grounded && rb.velocity.y < 0)
        {
            islide = true;
        }
        else
        {
            islide = false;
        }
    }
    void Update()
    {
        CheckIfSliding();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("DashMove");
        }
        if (Grounded || islide) Jumps = ExtraJumpCount;
        if (Input.GetKeyDown(KeyCode.UpArrow) && Jumps > 0)
        {
            Jumping = true;
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && Jumps == 0 && Grounded)
        {
            Jump();
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
    public void Jump()
    {

        if ((islide || touch) )
        {
            islide = false;
            Jumps--;
            StartCoroutine("WallJump");
        }
        else
        {
            rb.velocity = Vector2.up * JumpForce;
            Jumps--;
        }
    }

    IEnumerator WallJump()
    {
        for (int i = 0; i < 24; i++)
        {
            rb.AddForce(facingRight ? Vector2.left * WallJumpForce : Vector2.right * WallJumpForce);
            yield return new WaitForSeconds(0.005f);
        }
        Flip();
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
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("TestWalls"))
        {
            touch = true;
        }

    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("TestWalls"))
        {
            touch = false;
        }
    }
}
