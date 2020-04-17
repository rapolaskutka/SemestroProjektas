using UnityEngine;

public class CloneTeleport : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private GameObject impact;
    private Rigidbody2D rb;
    private Throwing throwclass;
    public Vector2 Pos;
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
            Instantiate(impact, transform.position, Quaternion.identity);
            throwclass.RemoveCooldownGhost();
            throwclass.ChangePos(transform.position);
            Destroy(this.gameObject);
        }

    }
}
