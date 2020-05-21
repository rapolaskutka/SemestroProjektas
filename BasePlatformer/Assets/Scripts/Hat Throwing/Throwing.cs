using UnityEngine;

public class Throwing : MonoBehaviour
{
    public Transform StartPoint;
    public float HatCD;
    private float CooldownTimer;
    private float GhostCD;
    public GameObject HatPrefab;
    public GameObject GhostClone;
    [HideInInspector] public bool Threwright;
    private bool TeleportEnabled = false;
    private AudioSource throwSound;
    private AudioClip throwclip;
    private AudioSource throwSound2;
    private AudioClip throwclip2;
    private void Start()
    {
        throwclip = Resources.Load<AudioClip>("Audio/HatThrow");
        throwSound = Addsound.AddAudio(throwclip, false, 0.65f, gameObject);
        throwclip2 = Resources.Load<AudioClip>("Audio/GhostShoot");
        throwSound2 = Addsound.AddAudio(throwclip2, false, 1f, gameObject);
    }
    void Update()
    {
        CooldownTimer -= Time.deltaTime;
        GhostCD -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z) && CooldownTimer < 0)
        {
            Instantiate(HatPrefab, StartPoint.position, StartPoint.rotation);
            CooldownTimer = HatCD;
            throwSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.X) && GhostCD < 0 && TeleportEnabled)
        {
            Threwright = FindObjectOfType<CharacterMovement>().facingRight;
            Instantiate(GhostClone, transform.position, transform.rotation);
            GhostCD = 4f;
            throwSound2.Play();
        }

    }
    public void RemoveCooldown()
    {
        CooldownTimer = 0;
    }
    public void RemoveCooldownGhost()
    {
        GhostCD = 0f;
    }
    public void ChangePos(Vector3 xd)
    {
        transform.position = xd;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnableTeleport")) TeleportEnabled = true;
        if (collision.CompareTag("DisableTeleport")) TeleportEnabled = false;

    }
}
