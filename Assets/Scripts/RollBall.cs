using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RollBall: MonoBehaviour
{
    private float _speed;
    private int initialPitch = 1;
    private Rigidbody _rigidBody;
    [SerializeField]
    private AudioSource ballRolling ;  
    [SerializeField]
    private AudioSource ballColliding ;
    
    private float volume;
    private float volumeColliding;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

    }

    private void Update()
    {

        volume = Mathf.Clamp01(_speed/6);
        
        volumeColliding = Mathf.Clamp01(_speed/8);
        
        
        if (ballRolling.isPlaying && _speed >= 0.1f)
        {
            ballRolling.volume = volume;

        }
        
             
        else if (ballRolling.isPlaying && _speed < 0.1f)
        {
            ballRolling.volume = 0;
        }    
    }



    void FixedUpdate()
    {
        _speed = _rigidBody.velocity.magnitude;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ballRolling.Play();

        }
        else if(!other.gameObject.CompareTag("Player"))
        {
            ballColliding.volume = volumeColliding;
            Debug.Log(volumeColliding);
            ballColliding.Play();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ballRolling.Stop();

        }    
    }
}