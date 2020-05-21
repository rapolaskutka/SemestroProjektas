using UnityEngine;

public class CloneTeleport : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private GameObject impact;
    [SerializeField] private GameObject BadImpact;
    private Rigidbody2D rb;
    private Throwing throwclass;
    private AudioSource badimpact;
    private AudioClip bimpactclip;
    private AudioSource goodimpact;
    private AudioClip goodclip;
    void Start()
    {
        bimpactclip = Resources.Load<AudioClip>("Audio/BadImpact");
        goodclip = Resources.Load<AudioClip>("Audio/GhostHit");
        badimpact = Addsound.AddAudio(bimpactclip, false, 1f, GameObject.FindGameObjectWithTag("Player"));
        goodimpact = Addsound.AddAudio(goodclip, false, 1f, GameObject.FindGameObjectWithTag("Player"));
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
            if (Vector3.Distance(movementclass.transform.position, transform.position) < 0.6f)
            {
                Instantiate(BadImpact, transform.position, Quaternion.identity);
                badimpact.Play();
                return;
            }
            Instantiate(impact, transform.position, Quaternion.identity);
            goodimpact.Play();
            Rigidbody2D PlayerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            movementclass.Jumps = 2;
            PlayerRb.velocity = Vector2.zero;
            if (throwclass.Threwright) throwclass.ChangePos(transform.position - (Vector3.right * 0.5f));
            else throwclass.ChangePos(transform.position + (Vector3.right * 0.7f));

        }

    }
}
