using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject particles;
    private Vector3 Position;
    void Start()
    {
        Position = transform.position;
        StartCoroutine(Spawning());
    }
    void Update() 
    {
    }

    IEnumerator Spawning()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        Position = new Vector3(16f, 9f, 0.0f);
        Instantiate(particles, Position, Quaternion.identity);
    }
}
