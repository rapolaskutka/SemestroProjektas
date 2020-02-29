using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMovement1 : MonoBehaviour
{
    public float speed;
    public float JumpForce;
    private float MoveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;    
    private bool Grounded;
    public Transform GroundCheck;
    public Transform CeilingCheck;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsCeiling;
    public LayerMask WhatIsWall;
    private int Jumps;
    private int JumpCount;
    private float JumpTimeCounter;
    public float JumpTime;
    private bool Jumping;
    private bool TouchRight;
    private bool TouchLeft;
    private bool HeadHitCheck;
    private bool Inwater;
    public float DashCooldown;
    private float DashCooldownTimer;
    private bool enabledDash = false;
    private void Start()
    {
        JumpCount = 1;
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
    }
    void Update()
    {
        if (Grounded ) Jumps = JumpCount;
        CoolDownTicker();
        CollisionChecks();
        DashCheck();
        JumpCheck();
        if (!Inwater) JumpHeightClocker();
        if (Input.GetKey(KeyCode.DownArrow) && Inwater) rb.gravityScale = 6;
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Jumping = false;
            JumpTimeCounter = JumpTime;

        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && Inwater) rb.gravityScale = 1f;
        if (HeadHitCheck)
        {
            JumpTimeCounter = 0;
            rb.velocity = Vector2.zero;
        }
    }

    private void CollisionChecks()
    {
        HeadHitCheck = Physics2D.OverlapCircle(CeilingCheck.position, 0.1f, WhatIsCeiling);
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, WhatIsGround);
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
            Jumping = true;
            rb.velocity = Vector2.up * JumpForce;
            if (!Inwater)
                StartCoroutine("RemoveJump");
        }
    }
    private void DashCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && DashCooldownTimer == 0 && enabledDash)
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


    IEnumerator RemoveJump()
    {
        yield return new WaitForSeconds(.1f);
        Jumps--;
    }
    IEnumerator DashMove()
    {
        speed += 20;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(.2f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        speed -= 20;
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
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
        if (collision.gameObject.tag == "EnableDash")
        {
            enabledDash = true;
        }
        if (collision.gameObject.tag == "EnableDoubleJump")
        {

            JumpCount = 2;
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
