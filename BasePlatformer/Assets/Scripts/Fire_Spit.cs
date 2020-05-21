using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Spit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float SpitAfter;
    [SerializeField] private float SpitEnd;
    [SerializeField] private Animator animator;
    private float startingTime;

    private bool canTakeDamage = false;

    private SpriteRenderer sprite;
    private Damage script;
    void Start()
    {
        startingTime = Time.time + SpitAfter;
        script = gameObject.GetComponent<Damage>();
        if(script.canTakeDamage)
        {
            script.canTakeDamage = false;
        }
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startingTime <= Time.time && canTakeDamage)
        {
            startingTime = Time.time + SpitAfter;
            // animator.WriteDefaultValues();
            sprite.enabled = false;
            animator.SetBool("Fire", false);
            script.canTakeDamage = false;
            canTakeDamage = false;
        }
        if(startingTime <= Time.time && !canTakeDamage)
        {
            sprite.enabled = true;
            startingTime = Time.time + SpitEnd;
            script.canTakeDamage = true;
            animator.SetBool("Fire", true);
            canTakeDamage = true;
        }
    }
}
