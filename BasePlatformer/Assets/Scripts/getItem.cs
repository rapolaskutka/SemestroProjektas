using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getItem : MonoBehaviour
{

    class Item
    {
        public string Name { get; set; }
        public int Count { get; private set; }

        Sprite sprite { get; set; }

        public SpriteRenderer render;

        public bool Placed { get; set; }

        public Item(string Name, Sprite sprite, int Count, SpriteRenderer sprit)
        {
            this.Name = Name;
            this.sprite = sprite;
            render = sprit;
            Count++;
        }

        public void ReplaceCount(int much)
        {
            Count += much;
        }
    }
    private List<Item> items;
    SpriteRenderer item;
    private GameObject cameras;
    // Start is called before the first frame update
    void Start()
    {
        items = new List<Item>();
        cameras = GameObject.FindGameObjectWithTag("MainCamera");
        item = cameras.gameObject.GetComponentsInChildren<SpriteRenderer>()[1];
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer render = collision.gameObject.GetComponent<SpriteRenderer>();
        if (collision.gameObject.tag.Equals("Item") && render != null)
        {
            SpriteRenderer wow = item;
            if (items.Count > 0)
            {
                wow.transform.position = new Vector3((float)items[items.Count - 1].render.transform.position.x, (float)items[items.Count - 1].render.transform.position.y - 1);
                wow.sprite = render.sprite;
                wow.transform.parent = cameras.transform;
                ContainItem(items, new Item(collision.gameObject.name, render.sprite, 1, wow));
            }
            else
            {
                wow.transform.position = new Vector3(item.transform.position.x, (float)item.gameObject.transform.position.y - items.Count);
                wow.sprite = render.sprite;
                wow.transform.parent = cameras.transform;
                items.Add(new Item(collision.gameObject.name, render.sprite, 1, wow));
            }

            Object.Destroy(collision.gameObject);
        }
    }
    public void Remove_Item(string name)
    {
        foreach(Item item in items)
        {
            if(item.Name.Equals("Key"))
            {
                items.Remove(item);
                Object.Destroy(item.render);
                break;
            }
        }
    }
    public bool Contain_Item(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name.Equals("Key"))
            {
                return true;
            }
        }
        return false;
    }
    private void ContainItem(List<Item> items, Item n_item)
    {
        foreach(Item item in items)
        {
            if(item.Name.Equals(n_item.Name))
            {
                item.ReplaceCount(n_item.Count);
            }
        }
        items.Add(n_item);
    }

}
