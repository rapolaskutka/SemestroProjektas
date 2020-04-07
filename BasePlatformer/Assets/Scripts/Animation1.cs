using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation1 : MonoBehaviour
{
    private SpriteRenderer Main_Sprite;
    private Sprite Starting;
    private int index = 0;
    private string[] Pictures_Tag =
    {
        "_0",
        "_1",
        "_2",
        "_3",
        "_4",
        "_5",
        "_6"
    };
    void Start()
    {
        Main_Sprite = GameObject.FindGameObjectWithTag("BackGround").GetComponent<SpriteRenderer>();
        Starting = Main_Sprite.sprite;
    }
    // Update is called once per frame
    void Update()
    {
        if (index == 0)
        {
            StartCoroutine(SpriteRender());
        }
    }
    private IEnumerator SpriteRender()
    {
        index = 1;
        for(int i =0; i < Pictures_Tag.Length- 1; i++)
        {
            string Name = Main_Sprite.sprite.name;
            string test = Name.Replace(Pictures_Tag[i], Pictures_Tag[i + 1]);
            Sprite new_sprite = Resources.Load<Sprite>("Hell/" + test);
            Main_Sprite.sprite = new_sprite;
            yield return new WaitForSeconds(0.5f);
        }
        Main_Sprite.sprite = Starting;
        index = 0;
    }
}
