using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    private static GM instance;
    [HideInInspector] public Vector2 StartingPos;
    [SerializeField] private GameObject particle;

    void Awake()
    {
        StartingPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);

        }
        else Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartingPos = transform.position;
            Instantiate(particle, transform.position, Quaternion.identity);

        }
    }
}
