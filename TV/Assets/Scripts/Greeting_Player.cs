using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Greeting_Player : MonoBehaviour
{
    private TextMeshPro[] welcomes;
    private Boss_Fight start;
    private bool Boss_Enabled;
    private GameObject tiles;
    void Start()
    {
        welcomes = new TextMeshPro[2];
        welcomes[0]= GameObject.Find("Welcome1").GetComponent<TextMeshPro>();
        welcomes[1] = GameObject.Find("Welcome2").GetComponent<TextMeshPro>();
        start = GameObject.Find("Boss").GetComponent<Boss_Fight>();
        tiles = GameObject.Find("Platforms");
        tiles.SetActive(false);
        Debug.Log(tiles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player") && gameObject.tag.Equals("Door") && !Boss_Enabled)
        {
            Boss_Enabled = true;
            welcomes[0].enabled = true;
            StartCoroutine(Next());
        }
        if(Boss_Enabled && collision.gameObject.tag.Equals("Player") && gameObject.tag.Equals("Door"))
        {
            collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x + 2, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
        }
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(3);
        welcomes[0].enabled = false;
        welcomes[1].enabled = true;
        StartCoroutine(Stop());
    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(3);
        welcomes[0].enabled = false;
        welcomes[1].enabled = false;
        tiles.SetActive(true);
        start.enabled = true;
    }
}
