using System;
using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

public class BallCollector : Switchable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Sphere"))
        {
            switchOn.Invoke();
        }
    }
}
