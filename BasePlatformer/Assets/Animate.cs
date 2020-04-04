using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    private SpriteRenderer Main_Sprite;
    private string A_Name;
    private string[] Pictures_Tag =
    {
        "_0",
        "_1",
        "_2",
        "_3",
        "_4",
        "_5",
        "_6",
        "_7"
    };

    void Start()
    {
        Main_Sprite = GameObject.FindGameObjectWithTag("BackGround").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i < Pictures_Tag.Length - 1; i++)
        {
            string Name = Main_Sprite.sprite.name.Replace(Pictures_Tag[i], Pictures_Tag[i + 1]);
            Main_Sprite.sprite.name = Name;
            if(i == 5)
            {
                i = 0;
            }
        }
    }
}
