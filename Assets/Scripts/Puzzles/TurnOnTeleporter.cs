using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnTeleporter : MonoBehaviour
{

    public GameObject teleporter;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("KOBE");
        teleporter.SetActive(true);    }

    
}
