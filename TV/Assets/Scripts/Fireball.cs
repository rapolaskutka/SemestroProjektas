using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private Rigidbody2D rb;
    private AudioSource fireballsound;
    private AudioClip clip;
    void Start()
    {
        clip = Resources.Load<AudioClip>("Audio/Fireball");
        Debug.Log(clip);
        fireballsound = Addsound.AddAudio(clip, false,0.8f,gameObject);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        fireballsound.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Player")) Destroy(gameObject); 
    }
}
