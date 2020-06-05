using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingSound : MonoBehaviour
{

    public AudioSource collisionSound;
    // Start is called before the first frame update


    private void OnCollisionEnter(Collision other)
    {
        // Debug.Log("Strange Man Touched me");
        collisionSound.Play();
    }
}
