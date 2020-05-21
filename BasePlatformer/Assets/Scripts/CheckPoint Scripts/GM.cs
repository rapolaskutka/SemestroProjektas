using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    private static GM instance;
    [HideInInspector] public Vector2 StartingPos;
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
}
