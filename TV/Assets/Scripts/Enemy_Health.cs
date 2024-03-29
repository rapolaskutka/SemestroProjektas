﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] private GameObject particles;
    private Transform PlayerPosition;
    private float hCool;
    private GameObject mesh;
    private TextMeshPro object_mesh;
    private GameObject heart;

    private AudioSource deathsound;
    private AudioClip clip;
    void Start()
    {
        clip = Resources.Load<AudioClip>("Audio/Enemydeath");
        deathsound = Addsound.AddAudio(clip,false,0.7f,gameObject);
        PlayerPosition = GetComponent<Transform>();
        mesh = Instantiate(GameObject.FindGameObjectWithTag("Enemy_Health"));
        heart = Instantiate(GameObject.Find("enemy_heart"));
        object_mesh = mesh.GetComponent<TextMeshPro>();
        object_mesh.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        mesh.transform.position = new Vector3(transform.position.x, transform.position.y + (float)0.5, transform.position.z);
        heart.transform.position = new Vector3(transform.position.x + (float)0.6, transform.position.y + (float)0.75, transform.position.z);
    }
    public bool GetDamage(int amount, bool knockback, float DamageCooldown)
    {
        if (hCool >= Time.time)
            return false;

        hCool = Time.time + DamageCooldown;
        if (amount >= health)
        {
            deathsound.Play();
            health = 0;
            Destroy(heart);
            Destroy(mesh);
            Destroy(gameObject);
            Instantiate(particles, PlayerPosition.position, Quaternion.identity);
        }
        else
        {
            object_mesh.text = (int.Parse(object_mesh.text) - amount).ToString();
            health -= amount;
        }
        return true;
    }
}
