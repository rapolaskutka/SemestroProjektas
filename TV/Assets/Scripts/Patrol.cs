using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    private bool movingRight = true;
    public Transform Ground;
    public GameObject deathparticles;
    [SerializeField]
    private LayerMask Hitswhat;
    [SerializeField]
    private LayerMask PlayerMask;
    [SerializeField]
    private bool CanShoot;
    [SerializeField]
    private Transform StartPoint;
    [SerializeField]
    private GameObject FireballPrefab;
    [SerializeField]
    private float Cooldown;
    [SerializeField]
    private bool CanSeeBehind;
    [SerializeField]
    private bool StartsLookingRight;
    private float CooldownTimer;
    private float DetectionRange;
    private RaycastHit2D ground, personright, personleft,Wall;
    private bool turning;
    [SerializeField]
    private float RotateTime;
    private bool isShoot;
    [SerializeField]
    private bool Stationary;
    private Enemy_Health health;


    private void Start()
    {
        if (!StartsLookingRight) Flip();
        if (CanShoot) DetectionRange = 10f;
        else DetectionRange = 0.5f;
        if (Stationary) speed = 0;
        health = GetComponent<Enemy_Health>();
        
    }

    private void Update()
    {
        CoolDownTicker();
        if(!turning)
        transform.Translate(Vector2.right * speed * Time.deltaTime); // move

        CollisionDetection();

        if (ground.collider == false && !turning && !isShoot)
        {
            turning = true;
            StartCoroutine(TurnAround());
        }
        if (Wall.collider == true && !turning && !isShoot)
        {
            turning = true;
            StartCoroutine(TurnAround());
        }
        if ((personright.collider == true || personleft.collider == true) && !CanShoot) Flip(); // avoids player if shooting turned off
        ShootingMechanics();
    }
    private IEnumerator TurnAround()
    {
        yield return new WaitForSeconds(RotateTime);

        if(!isShoot)
        Flip();

        turning = false;
    }
    private void CollisionDetection()
    {
        ground = Physics2D.Raycast(Ground.position, Vector2.down, 0.5f, Hitswhat);
        Wall = Physics2D.Raycast(Ground.position, Vector2.right, 0.5f, Hitswhat);
        personright = Physics2D.Raycast(Ground.position, Vector2.right, DetectionRange, PlayerMask);
        personleft = Physics2D.Raycast(Ground.position, Vector2.left, DetectionRange, PlayerMask);
    }

    private void CoolDownTicker()
    {
        if (CooldownTimer > 0) CooldownTimer -= Time.deltaTime;
        if (CooldownTimer < 0) CooldownTimer = 0;
    }

    private void Flip()
    {
        if (movingRight == true)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -0, 0);
            movingRight = true;
        }
    }
    private void ShootingMechanics()
    {
        if (CanShoot)
        {
            if ((personright.collider == false && personleft.collider == false) && !Stationary) speed = 1f; // moves if nobody is detected

            if (CanSeeBehind)
            {
                if ((personright.collider == true || personleft.collider == true))
                {
                    if ((personright.collider == true && movingRight == false)) Flip();
                    if ((personleft.collider == true && movingRight == true)) Flip();
                    if (CooldownTimer == 0)
                    {
                        Instantiate(FireballPrefab, StartPoint.position, StartPoint.rotation);
                        CooldownTimer = Cooldown;
                    }
                    speed = 0f;
                }
            }
            else 
            {
                if ((personright.collider == true && movingRight) || (personleft.collider == true && !movingRight))
                {
                    isShoot = true;
                    if (CooldownTimer == 0)
                    {
                        Instantiate(FireballPrefab, StartPoint.position, StartPoint.rotation);
                        CooldownTimer = Cooldown;
                    }
                    speed = 0f;
                }
                else
                {
                    isShoot = false;
                }
            }
           
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hat"))
        {
            health.GetDamage(30, false, (float)0.1);
        }
    }
}
