using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Info : MonoBehaviour
{
    public int health = -1;
    public Vector3 position;
    public static Info instance;
    void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Awake");
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        health = -1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void Start()
    {

    }
}
