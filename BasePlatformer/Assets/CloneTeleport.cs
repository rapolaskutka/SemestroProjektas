using UnityEngine;

public class CloneTeleport : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private GameObject impact;
    private Rigidbody2D rb;
    private Rigidbody2D PlayerRb;
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
            PlayerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            Instantiate(impact, transform.position, Quaternion.identity);
            throwclass.RemoveCooldownGhost();
            PlayerRb.velocity = Vector2.zero;
            if(throwclass.Threwright) throwclass.ChangePos(transform.position - (Vector3.right * 0.3f));
            else throwclass.ChangePos(transform.position + (Vector3.right * 0.3f));

            Destroy(this.gameObject);
        }

    }
}
