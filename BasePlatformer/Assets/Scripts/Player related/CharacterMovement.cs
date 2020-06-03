using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private Transform CeilingCheck;
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
    private bool JumpRequest;
    private float DefaultFall;
    private bool DashEnabled;
    private AudioSource jumpsound;
    private AudioClip jumpclip;
    private AudioSource hitSound;
    private AudioClip hitclip;

    private void Start()
    {
        jumpclip = Resources.Load<AudioClip>("Audio/JS");
        hitclip = Resources.Load<AudioClip>("Audio/oof");
        jumpsound = Addsound.AddAudio(jumpclip, false, 0.8f,gameObject);
        hitSound = Addsound.AddAudio(hitclip, false, 0.8f,gameObject);
        DashEnabled = false;
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
            jumpsound.Play();
        }

        FallSpeed();
    }
    void FallSpeed()
    {

        if (top)
        {
            if (rb.velocity.y < -5)
            {
                rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplierFloat - 1) * Time.deltaTime;
            }
            if (rb.velocity.y < 0 && !Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W"))))
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
            if (rb.velocity.y > 0 && !Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W"))))
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
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W"))) && Jumps > 0)
        {
            animatorss.SetTrigger("Trigger");
            Jumping = true; JumpRequest = true;
        }

        if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W"))) && Jumps > 0)  //(PlayerPrefs.GetString("Up")) 
        {
            Jumping = false;
            Jumps--;
        }
        if (!Grounded && Jumps == 2 && Jumping == false) Jumps = 1;

    }
    private void CollisionChecks()
    {
        HeadHitCheck = Physics2D.OverlapCircle(GroundCheck.position, 0.05f, WhatIsCeiling);
        if (HeadHitCheck)
        {
            rb.velocity = Vector2.zero;
        }
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.15f, WhatIsGround);

        // TouchLeft = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x - 0.35f, transform.position.y - 0.1f), WhatIsWall);

        //    TouchRight = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x + 0.35f, transform.position.y - 0.1f), WhatIsWall);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y + 0.1f), new Vector2(transform.position.x + 0.35f, transform.position.y - 0.1f));
    }
    private void DashCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && DashCooldownTimer == 0 && DashEnabled)
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

        if (collision.CompareTag("EnableDash")) DashEnabled = true;
        if (collision.gameObject.tag.Equals("Dead") || collision.gameObject.tag.Equals("Enemy")) 
        {
            hitSound.Play();
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

}
