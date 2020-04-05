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
    private bool Shooting;
    [SerializeField]
    private Transform StartPoint;
    [SerializeField]
    private GameObject FireballPrefab;
    [SerializeField]
    private float Cooldown;
    private float CooldownTimer;
    private float DetectionRange;
    private RaycastHit2D ground, personright, personleft;
    private void Start()
    {
        if (Shooting) DetectionRange = 10f;
        else DetectionRange = 0.3f;
    }

    private void Update()
    {
        CoolDownTicker();
        transform.Translate(Vector2.right * speed * Time.deltaTime); // move
        CollisionDetection();
        if (ground.collider == false) Flip(); // if no ground, turns around
        if ((personright.collider == true || personleft.collider == true) && !Shooting) Flip(); // avoids player if shooting turned off
       ShootingMechanics();

    }
    private void CollisionDetection()
    {
        ground = Physics2D.Raycast(Ground.position, Vector2.down, 2f, Hitswhat);
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
        if (Shooting)
        {
            if ((personright.collider == false && personleft.collider == false)) speed = 1f; // moves if nobody is detected
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
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hat"))
        {
            Instantiate(deathparticles, transform.position, Quaternion.identity);
        }
    }
}
