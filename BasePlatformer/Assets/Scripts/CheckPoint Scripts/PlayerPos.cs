using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    private GM gamemaster;
    void Start()
    {
        gamemaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        transform.position = gamemaster.CheckpointPos;
    }
}
