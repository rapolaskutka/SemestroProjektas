using UnityEngine;

public class CloneTeleport : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private GameObject impact;
    private Rigidbody2D rb;
    private Throwing throwclass;
    void Start()
    {
        throwclass = FindObjectOfType<Throwing>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            CharacterMovement movementclass = FindObjectOfType<CharacterMovement>();
            Rigidbody2D PlayerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            movementclass.Jumps = 2;
            Instantiate(impact, transform.position, Quaternion.identity);
            throwclass.RemoveCooldownGhost();
            PlayerRb.velocity = Vector2.zero;
            if(throwclass.Threwright) throwclass.ChangePos(transform.position - (Vector3.right * 0.7f ));
            else throwclass.ChangePos(transform.position + (Vector3.right * 0.7f ));
            Destroy(this.gameObject);
        }

    }
}
