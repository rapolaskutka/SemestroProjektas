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
    public float wallHopF;
    public float wallJumpF;
    public float wallDistance;
    private int facedir = 1;
    private bool touch;
    public Vector2 wallHopD;
    public Vector2 wallJumpD;
    public float wallSlideSpeed;
    private float moveDir;
    private bool islide;
    private void Start()
    {
        Jumps = ExtraJumpCount;
        rb = GetComponent<Rigidbody2D>();
        wallHopD.Normalize();
        wallJumpD.Normalize();

    }
    void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, WhatIsGround);
        MoveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
        if (!facingRight && MoveInput > 0) Flip();
        else if (facingRight && MoveInput < 0) Flip();
        ApplyMovement();

    }
    public void ApplyMovement()
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
        moveDir = Input.GetAxisRaw("Horizontal");
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
    public void Jump()
    {

        if (islide && moveDir == 0)
        {
            islide = false;
            Jumps--;
            Vector2 force = new Vector2(wallHopF * wallHopD.x * -facedir, wallHopF * wallHopD.y);
            rb.AddForce(force, ForceMode2D.Impulse);
        }
        else if ((islide || touch) && moveDir != 0)
        {
            islide = false;
            Jumps--;
            //Vector2 force = new Vector2(wallJumpF * wallJumpD.x * -moveDir, wallJumpF * wallJumpD.y);
            rb.AddForce(facingRight ? Vector2.left * wallJumpF : Vector2.right * wallJumpF);
        }
        else
        {
            rb.velocity = Vector2.up * JumpForce;
            Jumps--;
        }
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Bruh"))
        {
            touch = true;
        }

    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Bruh"))
        {
            touch = false;
        }
    }
    IEnumerator DashMove()
    {
        speed += 30;
        yield return new WaitForSeconds(.3f);
        speed -= 30;
    }
    void Flip()
    {
        if(islide)
        {
            facedir *= -1;
        }
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
