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
    public int JumpCount;
    private float JumpTimeCounter;
    public float JumpTime;
    private bool Jumping;
    public float WallJumpForce;
    private bool TouchRight;
    private bool TouchLeft;
    public float wallSlideSpeed;
    private bool islide;

    public float DashCooldown;
    private float DashCooldownTimer;
    private void Start()
    {
        Jumps = JumpCount;
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
    void Update()
    {
        CoolDownTicker();
        CollisionChecks();
        DashCheck();
        if (Grounded ) Jumps = JumpCount;
        if (TouchLeft || TouchRight) Jumps = JumpCount - 1;
        JumpCheck();
        JumpHeightClocker();
        if (Input.GetKeyUp(KeyCode.UpArrow)) { Jumping = false; JumpTimeCounter = JumpTime; }
    }

    private void JumpHeightClocker()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Jumping)
        {
            if (JumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * JumpForce;
                JumpTimeCounter -= Time.deltaTime;
            }
            else Jumping = false;

        }
    }

    private void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && Jumps > 0)
        {
            Jumping = true;
            Jump();
        }
    }
    public void Jump()
    {

        if ((TouchLeft || TouchRight))
        {
            StartCoroutine("WallJump");
        }
        else
        {
            rb.velocity = Vector2.up * JumpForce;
            StartCoroutine("RemoveJump");
        }
    }


    private void DashCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && DashCooldownTimer == 0)
        {
            StartCoroutine("DashMove");
            DashCooldownTimer = DashCooldown;
        }
    }

    private void CoolDownTicker()
    {
        if (DashCooldownTimer > 0) DashCooldownTimer -= Time.deltaTime;
        if (DashCooldownTimer < 0) DashCooldownTimer = 0;
    }

   
   
    IEnumerator WallJump()
    {

        islide = false;
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
        StartCoroutine("RemoveJump");

    }
    IEnumerator RemoveJump() 
    {
        yield return new WaitForSeconds(.1f);
        Jumps--;
    }
    IEnumerator DashMove()
    {
        speed += 30;
        yield return new WaitForSeconds(.2f);
        speed -= 30;
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    private void CollisionChecks()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && (TouchLeft || TouchRight) && !Grounded && rb.velocity.y < 0)
        {
            islide = true;
        }
        else
        {
            islide = false;
        }
        TouchLeft = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x - 0.45f, transform.position.y - 0.1f), WhatIsWall);
        TouchRight = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x + 0.45f, transform.position.y - 0.1f), WhatIsWall);
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

}
