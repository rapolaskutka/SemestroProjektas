using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private Rigidbody2D rb;
    private AudioSource fireballsound;
    private AudioClip clip;
    private bool Collided = false;
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
        if ((collision.CompareTag("Ground") || collision.CompareTag("Player")) && !Collided)
        {
            GameObject fire = Instantiate(GameObject.Find("Fire_Explosion"));
            fire.transform.position = gameObject.transform.position;
            fire.GetComponent<ParticleSystem>().Play();
            this.GetComponent<SpriteRenderer>().enabled = false;
            Collided = true;
            StartCoroutine(Remove(fire));
        }
    }
    IEnumerator Remove(GameObject fire)
    {
        yield return new WaitForSeconds((float)0.6);
        Destroy(fire);
        Destroy(gameObject);
    }
}
