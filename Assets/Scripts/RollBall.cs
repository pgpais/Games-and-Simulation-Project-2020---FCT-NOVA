using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RollBall: MonoBehaviour
{
    private float _speed;
    private int initialPitch = 1;
    private Rigidbody _rigidBody;
    private AudioSource _ballSoundSource ;
    public float volume;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _ballSoundSource = GetComponent<AudioSource>();

    }

    private void Update()
    {

        volume = Mathf.Clamp01(_speed/6);
        
        
        if (_ballSoundSource.isPlaying && _speed >= 0.1f)
        {
            _ballSoundSource.volume = volume;

        }
        
             
        else if (_ballSoundSource.isPlaying && _speed < 0.1f)
        {
            _ballSoundSource.volume = 0;
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
            _ballSoundSource.Play();

        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _ballSoundSource.Stop();

        }    }
}