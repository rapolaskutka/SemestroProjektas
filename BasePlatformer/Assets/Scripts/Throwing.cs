using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    public Transform StartPoint;

    public GameObject HatPrefab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shoot();
        }
        
    }
    void Shoot() 
    {
        Instantiate(HatPrefab, StartPoint.position, StartPoint.rotation);
    }
}
