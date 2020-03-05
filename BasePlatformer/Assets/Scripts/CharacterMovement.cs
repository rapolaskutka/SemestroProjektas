using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform GroundCheck;
    [SerializeField]
    private LayerMask WhatIsGround;
    [SerializeField]
    private LayerMask WhatIsCeiling;
    [SerializeField]
    private LayerMask WhatIsWall;
    [SerializeField]
    private int JumpCount;
    [SerializeField]
    private float JumpTime;
    [SerializeField]
    private float DashCooldown;
    private Animator animatorss;
    private Rigidbody2D rb;
    private bool Grounded;
    public float MoveInput;
    public float JumpTimeCounter;
    private bool Jumping;
    private bool HeadHitCheck;
    private bool Inwater;
    private float DashCooldownTimer;
    private int Jumps;
    private bool Moving;
    public float JumpForce;
    public bool facingRight = true;
    public bool top;


    //private float wallSlideSpeed;
    //private bool TouchRight;
    //private bool TouchLeft;
    //private bool islide;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatorss = GetComponent<Animator>();
        JumpTimeCounter = JumpTime;
        Jumps = JumpCount;
    }
    void FixedUpdate()
    {

        MoveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
        //ApplySliding();

    }
    void Update()
    {
        if (Grounded) { Jumps = JumpCount; JumpTimeCounter = JumpTime; }
        CoolDownTicker();
        CollisionChecks();
        DashCheck();
        Jump();
        WaterDive();
        Animations();
    }

    private void Animations()
    {
        if (MoveInput != 0) Moving = true;
        else Moving = false;
        animatorss.SetBool("Moving", Moving);
        animatorss.SetBool("Dashing", Dashing);
        if (!facingRight && MoveInput > 0) Flip();
        else if (facingRight && MoveInput < 0) Flip();
    }

    private void WaterDive()
    {
        if (Input.GetKey(KeyCode.DownArrow) && Inwater) rb.gravityScale = 6;
        if (Input.GetKeyUp(KeyCode.DownArrow) && Inwater) rb.gravityScale = 1f;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && Jumps > 0)
        {
            StartCoroutine("RemoveJump");
            animatorss.SetTrigger("Trigger");
            Jumping = true;

        }
        if (Input.GetKey(KeyCode.UpArrow) && Jumping && JumpTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            JumpTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && Jumps > 0)
        {
            Jumping = false;
            JumpTimeCounter = JumpTime;
        }
    }
    private void CollisionChecks()
    {
        HeadHitCheck = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.10f, transform.position.y + 0.2f), new Vector2(transform.position.x + 0.2f, transform.position.y + 0.25f), WhatIsCeiling);
        if (HeadHitCheck)
        {
            JumpTimeCounter = 0;
            rb.velocity = Vector2.zero;
        }
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, WhatIsGround);
        //if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && (TouchLeft || TouchRight) && !Grounded && rb.velocity.y < 0)
        //    islide = true;
        //else
        //    islide = false;
        //TouchLeft = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x - 0.35f, transform.position.y - 0.1f), WhatIsWall);

        //TouchRight = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x + 0.35f, transform.position.y - 0.1f), WhatIsWall);

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
    IEnumerator RemoveJump()
    {
        yield return new WaitForSeconds(.05f);
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
//public void ApplySliding()
//{
//    if (islide)
//    {

//        if (rb.velocity.y < -wallSlideSpeed)
//        {
//            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
//        }
//    }
//}