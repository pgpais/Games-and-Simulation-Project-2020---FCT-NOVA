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

    [SerializeField][Range(1,10)] private float rollingQuietness = 6;
    [SerializeField][Range(1,10)] private float collidingQuietness = 8;
    
    private float _volume;
    private float _volumeColliding;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

    }

    private void Update()
    {

        _volume = Mathf.Clamp01(_speed/rollingQuietness);
        
        _volumeColliding = Mathf.Clamp01(_speed/collidingQuietness);
        
        
        if (ballRolling.isPlaying && _speed >= 0.1f)
        {
            ballRolling.volume = _volume;

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
            ballColliding.volume = _volumeColliding;
            Debug.Log(_volumeColliding);
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