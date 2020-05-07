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
    void Update()
    {
        CooldownTimer -= Time.deltaTime;
        GhostCD -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z) && CooldownTimer < 0)
        {
            Instantiate(HatPrefab, StartPoint.position, StartPoint.rotation);
            CooldownTimer = HatCD;
        }
        if (Input.GetKeyDown(KeyCode.X) && GhostCD < 0 && TeleportEnabled)
        {
            Threwright = FindObjectOfType<CharacterMovement>().facingRight;
            Instantiate(GhostClone, transform.position, transform.rotation);
            GhostCD = 4f;
        }

    }
    public void RemoveCooldown()
    {
        CooldownTimer = 0;
    }
    public void RemoveCooldownGhost()
    {
        GhostCD = 0.2f;
    }
    public void ChangePos(Vector3 xd)
    {
        transform.position = xd;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnableTeleport")) TeleportEnabled = true;
    }
}
