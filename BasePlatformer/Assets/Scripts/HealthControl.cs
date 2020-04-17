using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthControl : MonoBehaviour
{

    [SerializeField] private float DamageCooldown;
    [SerializeField] public int health;
    [SerializeField] private GameObject blood;
    private List<SpriteRenderer> healths = new List<SpriteRenderer>();
    private GameObject UI;
    private Transform PlayerPosition;
    private float hCool;

    void Start()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        SpriteRenderer rend = camera.gameObject.GetComponentInChildren<SpriteRenderer>();
        double x = 0;
        healths.Add(rend);
        for (int i = 0; i < health - 1; i++)
        {
            x += 0.7;
            SpriteRenderer obje = GameObject.Instantiate(rend);
            obje.transform.position = new Vector3(rend.transform.position.x + (float)x, rend.transform.position.y, rend.transform.position.z);
            healths.Add(obje);
            obje.transform.parent = camera.transform;

        }
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        UI.SetActive(false);
    }
    public bool GetDamage(int amount, bool knockback, float DamageCooldown)
    {
        if (hCool >= Time.time)
            return false;

        hCool = Time.time + DamageCooldown;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SpriteRenderer sprite = player.GetComponent<SpriteRenderer>();
        StartCoroutine(DamageIndicator(sprite, DamageCooldown));
        sprite.enabled = true;
        if (amount >= health)
        {
            for (int i = 0; i < amount; i++)
            {
                healths[i].enabled = false;
            }
            health = 0;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            Instantiate(blood, PlayerPosition.position, Quaternion.identity);
            UI.SetActive(true);
            return true;
        }
        else
        {
            for (int i = 0; i < health; i++)
            {
                if (i + amount >= health)
                {
                    healths[i].enabled = false;
                    
                }
            }
            health -= amount;;
        }
        return false ;
    }
    private void Awake()
    {
        UI = GameObject.FindGameObjectWithTag("DScreen");
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
