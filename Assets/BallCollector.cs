using System;
using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

public class BallCollector : Switchable
{
    public GameObject ball;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(ball))
        {
            switchOn.Invoke();
        }
    }
}
