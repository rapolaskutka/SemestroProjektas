using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    public Transform StartPoint;
    public float Cooldown;
    private float CooldownTimer;

    public GameObject HatPrefab;
    void Update()
    {
        if (CooldownTimer > 0) CooldownTimer -= Time.deltaTime;
        if (CooldownTimer < 0) CooldownTimer = 0;
        if (Input.GetKeyDown(KeyCode.Z) && CooldownTimer == 0)
        {
            Instantiate(HatPrefab, StartPoint.position, StartPoint.rotation);
            CooldownTimer = Cooldown;
        }
        
    }
    public void RemoveCooldown() 
    {
        CooldownTimer = 0;
    }
}
