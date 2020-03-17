using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 10f;

    private float movH, movV; 
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movH = Input.GetAxisRaw("Horizontal");
        movV = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 mov = transform.forward * movV + transform.right * movH;
        mov.Normalize();
        mov *= speed;
        mov += new Vector3(0, rb.velocity.y, 0);
        rb.velocity = mov;
    }
}
