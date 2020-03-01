using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    public float JumpForce;
    private float MoveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;
    [SerializeField] bool Grounded;
    public Transform GroundCheck;
    public Transform CeilingCheck;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsCeiling;
    public LayerMask WhatIsWall;
    [SerializeField] int Jumps;
    public int JumpCount;
    private float JumpTimeCounter;
    public float JumpTime;
    private bool Jumping;
    public float WallJumpForce;
    [SerializeField] bool TouchRight;
    [SerializeField] bool TouchLeft;
    public float wallSlideSpeed;
    private bool islide;
    public bool HeadHitCheck;
    public bool Inwater;
    public Animator animatorss;
    public float DashCooldown;
    private float DashCooldownTimer;
    private void Start()
    {
        Jumps = JumpCount;
        rb = GetComponent<Rigidbody2D>();
        JumpTimeCounter = JumpTime;

    }
    void FixedUpdate()
    {
        MoveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
        if (!facingRight && MoveInput > 0) Flip();
        else if (facingRight && MoveInput < 0) Flip();
        ApplySliding();
    }
    void Update()
    {
        if (Grounded) Jumps = JumpCount;
        if (TouchLeft || TouchRight) Jumps = JumpCount - 1;
        CoolDownTicker();
        CollisionChecks();
        DashCheck();
        JumpCheck();
        if (!Inwater) JumpHeightClocker();
        if (Input.GetKeyUp(KeyCode.UpArrow) && Jumps > 0)
        {
            Jumping = false;
            JumpTimeCounter = JumpTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) && Inwater) rb.gravityScale = 6;
        if (Input.GetKeyUp(KeyCode.DownArrow) && Inwater) rb.gravityScale = 1f;
        if (HeadHitCheck)
        {
            JumpTimeCounter = 0;
            rb.velocity = Vector2.zero;
        }

        animatorss.SetBool("Dashing", Dashing);
        animatorss.SetFloat("Moving", Mathf.Abs(MoveInput));
        
    }

    private void CollisionChecks()
    {
        HeadHitCheck = Physics2D.OverlapCircle(CeilingCheck.position, 0.1f, WhatIsCeiling);
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, WhatIsGround);
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && (TouchLeft || TouchRight) && !Grounded && rb.velocity.y < 0)
            islide = true;
        else
            islide = false;
        TouchLeft = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x - 0.35f, transform.position.y - 0.1f), WhatIsWall);

        TouchRight = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x + 0.35f, transform.position.y - 0.1f), WhatIsWall);

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
            animatorss.SetTrigger("Trigger");
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
            if (!Inwater) StartCoroutine("RemoveJump");

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
            for (int i = 0; i < 30; i++)
            {
                rb.AddForce(new Vector2(-WallJumpForce, 0));
                yield return new WaitForSeconds(0.005f);

            }
        }
        else
        {
            for (int i = 0; i < 30; i++)
            {
                rb.AddForce(new Vector2(WallJumpForce, 0));
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
    private bool Dashing = false;
    IEnumerator DashMove()
    {
        Dashing = true;
        speed += 20;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(.2f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        speed -= 20;
        Dashing = false;
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dead")
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collision.gameObject.tag == "TP")
        {
            SceneManager.LoadScene("Best");
        }
        if (collision.gameObject.tag == "Water")
        {
            Jumps = 9999;
            JumpCount = 9999;
            speed -= 2f;
            JumpForce -= 3;
            rb.gravityScale = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            StartCoroutine("Restore");
        }

    }

    IEnumerator Restore()
    {
        yield return new WaitForSeconds(.1f);
        Jumps = 1;
        JumpCount = 2;
        speed += 2f;
        JumpForce += 3;
        rb.gravityScale = 3f;
        Inwater = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Inwater = true;
        }
    }

}
