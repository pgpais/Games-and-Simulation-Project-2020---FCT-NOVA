using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private bool open = false;
    [SerializeField]
    public GameObject plate;

    public float height;

    public bool allowPlayer = false;

    public bool colorChange = false;
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I'm in");
        if (!open)
        {
            if (other.CompareTag("Carryable") || allowPlayer)
            {
                open = true;
                door.transform.position += new Vector3(0, height, 0);
                
                if(colorChange)
                    plate.GetComponent<Renderer>().material = Resources.Load("Materials/GreenMat", typeof(Material)) as Material;;

            }
            else
            {
                if(colorChange)

                    plate.GetComponent<Renderer>().material = Resources.Load("Materials/RedMat", typeof(Material)) as Material;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("I'm out");
        if (open)
        {
            door.transform.position += new Vector3(0, -height, 0);
            open = false;

        }
        if(colorChange)
            plate.GetComponent<Renderer>().material = Resources.Load("Materials/Grey", typeof(Material)) as Material;;
    }
}