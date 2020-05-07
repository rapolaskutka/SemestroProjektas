using UnityEngine;

public class CloneTeleport : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private GameObject impact;
    [SerializeField] private GameObject BadImpact;
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
            throwclass.RemoveCooldownGhost();
            Destroy(this.gameObject);
            if (Vector3.Distance(movementclass.transform.position, transform.position) < 0.5f)
            {
                Instantiate(BadImpact, transform.position, Quaternion.identity);
                return;
            }
            Instantiate(impact, transform.position, Quaternion.identity);
            Rigidbody2D PlayerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            movementclass.Jumps = 2;
            PlayerRb.velocity = Vector2.zero;
            if (throwclass.Threwright) throwclass.ChangePos(transform.position - (Vector3.right * 0.7f));
            else throwclass.ChangePos(transform.position + (Vector3.right * 0.7f));

        }

    }
}
