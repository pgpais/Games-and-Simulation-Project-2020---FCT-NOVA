using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingSound : MonoBehaviour
{

    public AudioSource collisionSound;
    // Start is called before the first frame update
    void Start()
    {
        collisionSound = GetComponent<AudioSource>();
    }
    

    private void OnCollisionEnter(Collision other)
    {
        collisionSound.Play();
    }
}
