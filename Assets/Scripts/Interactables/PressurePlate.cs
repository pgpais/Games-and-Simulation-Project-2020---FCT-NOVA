using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private bool open = false;
    [SerializeField]
    public GameObject box;
    [SerializeField]
    public Material wrong;
    [SerializeField]
    public Material right;
    [SerializeField]
    public Material normal;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I'm in");
        if (!open)
        {
            if (other.CompareTag("Carryable"))
            {
                open = true;
                door.transform.position += new Vector3(0, 4, 0);
                box.GetComponent<Renderer>().material = right;

            }
            else
            {
                box.GetComponent<Renderer>().material = wrong;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("I'm out");
        if (open)
        {
            door.transform.position += new Vector3(0, -4, 0);
            open = false;
            box.GetComponent<Renderer>().material = normal;

        }
    }
}