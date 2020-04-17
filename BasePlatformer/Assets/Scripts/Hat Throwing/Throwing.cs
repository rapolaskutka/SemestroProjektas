using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    public Transform StartPoint;
    public float HatCD;
    private float CooldownTimer;
    private float GhostCD;
    public GameObject HatPrefab;
    public GameObject GhostClone;
    public bool Threwright;
    void Update()
    {
        CooldownTimer -= Time.deltaTime;
        GhostCD -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z) && CooldownTimer < 0)
        {
            Instantiate(HatPrefab, StartPoint.position, StartPoint.rotation);
            CooldownTimer = HatCD;
        }
        if (Input.GetKeyDown(KeyCode.X) && GhostCD < 0)
        {
            Threwright = FindObjectOfType<CharacterMovement>().facingRight;
            Instantiate(GhostClone, StartPoint.position, StartPoint.rotation);
            GhostCD = 5f;
        }

    }
    public void RemoveCooldown()
    {
        CooldownTimer = 0;
    }
    public void RemoveCooldownGhost()
    {
        GhostCD = 0;
    }
    public void ChangePos(Vector3 xd)
    {
        transform.position = xd;
    }
}
