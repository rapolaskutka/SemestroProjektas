using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeOnStart : MonoBehaviour
{
    [SerializeField] private float FreezeTime;
    CharacterMovement cc;
    void Start()
    {
        cc = GetComponent(typeof(CharacterMovement)) as CharacterMovement;
        cc.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        FreezeTime -= Time.deltaTime;
        if (FreezeTime < 0) {
            cc.enabled = true;
            this.enabled = false;
        }
        
    }
}
