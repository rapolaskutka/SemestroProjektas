using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFallDown : MonoBehaviour
{
    private PlatformEffector2D effect;
    void Start()
    {
        effect = GetComponent<PlatformEffector2D>();
    }

  
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow)) effect.rotationalOffset = 180f;
        if (Input.GetKey(KeyCode.UpArrow)) effect.rotationalOffset = 0f;

    }
}
