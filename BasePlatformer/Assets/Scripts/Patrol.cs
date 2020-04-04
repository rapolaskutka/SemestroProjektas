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



    private void Start() 
    {
        if (Shooting) DetectionRange = 10f;
        else DetectionRange = 0.3f;
    }

    private void Update()
    {
        if (CooldownTimer > 0) CooldownTimer -= Time.deltaTime;
        if (CooldownTimer < 0) CooldownTimer = 0;
        Debug.Log(CooldownTimer);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D ground = Physics2D.Raycast(Ground.position, Vector2.down, 2f, Hitswhat);
        RaycastHit2D personright = Physics2D.Raycast(Ground.position, Vector2.right, DetectionRange, PlayerMask);
        RaycastHit2D personleft = Physics2D.Raycast(Ground.position, Vector2.left, DetectionRange, PlayerMask);
        if (ground.collider == false) Collision();
        if ((personright.collider  == true || personleft.collider == true) && !Shooting) Collision();
        if ((personright.collider == true || personleft.collider == true) && Shooting) Shoot();
    }

    private void Collision()
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
    private void Shoot()
    {
        if(CooldownTimer == 0) Instantiate(FireballPrefab, StartPoint.position, StartPoint.rotation);
        CooldownTimer = Cooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hat"))
        {
            Instantiate(deathparticles, transform.position, Quaternion.identity);
        }
    }
}
