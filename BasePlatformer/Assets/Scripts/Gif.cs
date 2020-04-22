using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gif : MonoBehaviour
{
    private string SpriteName;
    void Start()
    {
        SpriteRenderer Sprite = GameObject.FindObjectOfType<SpriteRenderer>();
        SpriteName = Sprite.sprite.name.Substring(0, Sprite.sprite.name.Length - 2);
    }

    void Update()
    {
        Debug.Log("Test");
        Debug.Log(SpriteName);
    }
}
