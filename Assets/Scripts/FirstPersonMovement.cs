using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;
    
    [SerializeField]
    private bool isGrounded = true;

    [Header("OverlapBox Points")]
    [SerializeField]
    private Transform GroundCheck;
    private Vector3 boxCenter;
    [SerializeField]
    private Vector3 boxRadiuses;
    [SerializeField]
    private LayerMask boxMask;

    private float movH, movV;
    private bool triedJumping;
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
        triedJumping = Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJumping();
    }

    private void HandleJumping()
    {
        CheckGrounded();
        if (triedJumping && isGrounded)
        {
            // TODO: check different ForceModes for different feels
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    private void CheckGrounded()
    {
        if (Physics.OverlapBox(GroundCheck.position, boxRadiuses, Quaternion.identity, boxMask).Length > 0)
        {
            //TODO: check that this work for every case (probably mess with Layermask)
            isGrounded = true; 
        }
        else
        {
            isGrounded = false;
        }
    }

    void HandleMovement()
    {
        Vector3 mov = transform.forward * movV + transform.right * movH;
        mov.Normalize();
        mov *= speed;
        mov += new Vector3(0, rb.velocity.y, 0);
        rb.velocity = mov;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        var position = GroundCheck.position;
        Gizmos.DrawSphere(position, 0.05f);
        Gizmos.DrawWireCube(position, boxRadiuses*2);
    }
}
