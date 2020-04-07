using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private bool open = false;
    
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I'm in");
        if (!open)
        {
            open = true;
            door.transform.position += new Vector3(0, 4, 0);
            transform.GetComponent<Renderer>().material.color = Color.red;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("I'm out");
        if (open)
        {
            door.transform.position += new Vector3(0, -4, 0);
            open = false;
        }
    }
}