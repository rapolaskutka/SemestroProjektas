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
    public LayerMask WhatIsGround;
    public LayerMask WhatIsWall;
    private int Jumps;
    public int ExtraJumpCount;
    private float JumpTimeCounter;
    public float JumpTime;
    private bool Jumping;
    public float WallJumpForce;
    private bool TouchRight;
    private bool TouchLeft;
    public float wallSlideSpeed;
    private bool islide;
    private void Start()
    {
        Jumps = ExtraJumpCount;
        rb = GetComponent<Rigidbody2D>();

    }
    void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, WhatIsGround);
        MoveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
        if (!facingRight && MoveInput > 0) Flip();
        else if (facingRight && MoveInput < 0) Flip();
        ApplySliding();

    }

    public void ApplySliding()
    {
        if (islide)
        {

            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }


        }
    }
    public void CheckIfSliding()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && (TouchLeft || TouchRight) && !Grounded && rb.velocity.y < 0)
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
        TouchLeft = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x - 0.6f, transform.position.y - 0.1f), WhatIsWall);
        TouchRight = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x + 0.6f, transform.position.y - 0.1f), WhatIsWall);
        CheckIfSliding();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("DashMove");
        }
        if (Grounded || TouchLeft || TouchRight) Jumps = ExtraJumpCount;
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

        if ((TouchLeft || TouchRight))
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
        if (TouchRight)
        {
            for (int i = 0; i < 20; i++)
            {
                rb.AddForce(Vector2.left * WallJumpForce);
                yield return new WaitForSeconds(0.005f);
            }
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {
                rb.AddForce(Vector2.right * WallJumpForce);
                yield return new WaitForSeconds(0.005f);
            }
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
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
