using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private LayerMask WhatIsCeiling;
    [SerializeField] private int ExtraJumpCount;
    [SerializeField] private float DashCooldown;
    [SerializeField] private float fallMultiplierFloat;
    [SerializeField] private float lowJumpMultiplierFloat;
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool top;
    [HideInInspector] public int Jumps;
    private Animator animatorss;
    private Rigidbody2D rb;
    private bool Grounded;
    private bool Jumping;
    private bool HeadHitCheck;
    private float DashCooldownTimer;

    private bool Moving;
    public float JumpForce;
    private float MoveInput;
    [SerializeField] private LayerMask WhatIsWall;
    private float wallSlideSpeed;
    private bool TouchRight;
    private bool TouchLeft;
    private bool islide;
    private bool JumpRequest;
    private float DefaultFall;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animatorss = GetComponent<Animator>();
        Jumps = ExtraJumpCount;
        DefaultFall = fallMultiplierFloat;
    }
    void FixedUpdate()
    {

        MoveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
        if (JumpRequest)
        {
            rb.velocity = Vector2.up * JumpForce;
            JumpRequest = false;
        }

        FallSpeed();
        ApplySliding();
    }
    void FallSpeed()
    {
       
        if (top)
        {
            if (rb.velocity.y < -5)
            {
                rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplierFloat - 1) * Time.deltaTime;
            }
            if (rb.velocity.y < 0 && !Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplierFloat - 1) * Time.deltaTime;
            }
        }
        else
        {
            if (rb.velocity.y < 5)
            {
                rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplierFloat - 1) * Time.deltaTime;
            }
            if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplierFloat - 1) * Time.deltaTime;
            }
        }
    }


    void Update()
    {
        if (Grounded) Jumps = ExtraJumpCount;
        CoolDownTicker();
        CollisionChecks();
        DashCheck();
        Jump();
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


    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && Jumps > 0)
        {
            animatorss.SetTrigger("Trigger");
            Jumping = true; JumpRequest = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && Jumps > 0)
        {
            Jumping = false;
            Jumps--;
        }
        if (!Grounded && Jumps == 2 && Jumping == false) Jumps = 1;

    }
    private void CollisionChecks()
    {
        HeadHitCheck = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.10f, transform.position.y + 0.2f), new Vector2(transform.position.x + 0.2f, transform.position.y + 0.25f), WhatIsCeiling);
        if (HeadHitCheck)
        {
            rb.velocity = Vector2.zero;
        }
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, WhatIsGround);
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && (TouchLeft || TouchRight) && !Grounded && rb.velocity.y < 0)
            islide = true;
        else
            islide = false;
        TouchLeft = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x - 0.35f, transform.position.y - 0.1f), WhatIsWall);
        TouchRight = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x + 0.35f, transform.position.y - 0.1f), WhatIsWall);
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            StartCoroutine("Restore");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Jumps = 9999;
            ExtraJumpCount = 9999;
            speed -= 2f;
            JumpForce -= 3;
            rb.gravityScale = 1f;
        }
        else if (collision.gameObject.tag.Equals("FallSpeed_In"))
        {
            fallMultiplierFloat = (float)0.25;
            Jumps = 0;
        }
        else if (collision.gameObject.tag.Equals("FallSpeed_Out"))
        {
            fallMultiplierFloat = DefaultFall;
        }
    }
    IEnumerator Restore()
    {
        yield return new WaitForSeconds(.1f);
        Jumps = 1;
        ExtraJumpCount = 2;
        speed += 2f;
        JumpForce += 3;
        rb.gravityScale = 3f;
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
