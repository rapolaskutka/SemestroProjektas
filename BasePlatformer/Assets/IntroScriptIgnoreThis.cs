using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScriptIgnoreThis : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Transition() 
    {
        yield return new WaitForSeconds(5f);
    }
}
