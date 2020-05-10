﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss_Health : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] private GameObject particles;
    private Transform PlayerPosition;
    private float hCool;
    private GameObject mesh;
    private TextMeshPro object_mesh;
    private GameObject heart;
    private GameObject head;
    void Start()
    {
        PlayerPosition = GetComponent<Transform>();
        mesh = Instantiate(GameObject.FindGameObjectWithTag("Enemy_Health"));
        heart = Instantiate(GameObject.FindGameObjectWithTag("Heart"));
        head = GameObject.Find("Head");
        object_mesh = mesh.GetComponent<TextMeshPro>();
        object_mesh.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        mesh.transform.position = new Vector3(head.transform.position.x, head.transform.position.y + (float) 1.0, 0);
        heart.transform.position = new Vector3(head.transform.position.x + (float)2.0, head.transform.position.y + (float)1.6, 0);
        mesh.transform.localScale = new Vector3(3, 3, 0);
        heart.transform.localScale = new Vector3((float)0.25, (float)0.25, 0);
    }
    public bool GetDamage(int amount, bool knockback, float DamageCooldown)
    {
        if (hCool >= Time.time)
            return false;

        hCool = Time.time + DamageCooldown;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(DamageIndicator(sprite, DamageCooldown));
        if (amount >= health)
        {
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
    private IEnumerator DamageIndicator(SpriteRenderer sprite, float second)
    {
        int total = (((int)second * 10) / 4);
        for (int i = 0; i < total; i++)
        {
            for (int b = 0; b < 1; b++)
            {
                sprite.enabled = false;
                yield return new WaitForSeconds(.1f);
            }
            sprite.enabled = true;
            yield return new WaitForSeconds(.3f);
        }
    }
}