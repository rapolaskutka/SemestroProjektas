using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthControl : MonoBehaviour
{
    public int health;
    List<SpriteRenderer> healths = new List<SpriteRenderer>();
    private GameObject UI;
    private float hCool;
    void Start()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        SpriteRenderer rend = camera.gameObject.GetComponentInChildren<SpriteRenderer>();
        double x = 0;
        healths.Add(rend);
        for (int i = 0; i < health - 1; i++)
        {
            x += 0.3;
            SpriteRenderer obje = GameObject.Instantiate(rend);
            obje.transform.position = new Vector3(rend.transform.position.x + (float)x, rend.transform.position.y, rend.transform.position.z);
            healths.Add(obje);
            obje.transform.parent = camera.transform;

        }
    }
    public void GetDamage(int amount, bool knockback, double dCool)
    {
        if (hCool >= Time.time)
            return;

        hCool = Time.time + (float)dCool;


        if (amount >= health)
        {
            for (int i = 0; i < amount; i++)
            {
                healths[i].enabled = false;
            }
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            health = 0;
            UI.SetActive(true);
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
            health -= amount;
            DamageCooldown(5.0);
        }
    }
    private void Awake()
    {
        UI = GameObject.FindGameObjectWithTag("DScreen");
    }
    IEnumerator DamageCooldown(double dCool)
    {
        yield return new WaitForSeconds(3f);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
