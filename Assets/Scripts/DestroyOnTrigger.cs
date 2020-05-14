using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectoToDestroy;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(objectoToDestroy);
    }
}