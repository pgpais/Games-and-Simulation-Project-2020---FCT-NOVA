using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonMovement : MonoBehaviourPun
{
    
    
    public float speed = 10f;
    public float jumpForce = 10f;

    [SerializeField] private bool isGrounded = true;

    [Header("OverlapBox Points")] [SerializeField]
    private Transform GroundCheck;

    private Vector3 boxCenter;
    [SerializeField] private Vector3 boxRadiuses;
    [SerializeField] private LayerMask boxMask;

    private float movH, movV;
    private bool triedJumping;
    private Rigidbody rb;
    public Rigidbody Rb => rb;
    [SerializeField]
    private Animator animator;

    private static readonly int Speed = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJumping();
    }

    #region Jumping

    public void ReceiveJumpInput()
    {
       //     Debug.Log("Wanna jump");
        triedJumping = true;
    }
    
    private void HandleJumping()
    {
        CheckGrounded();
        if (triedJumping && isGrounded)
        {
            // TODO: check different ForceModes for different feels
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        triedJumping = false;
    }

    private void CheckGrounded()
    {
        // Maybe nonAlloc
        isGrounded = Physics.OverlapBox(GroundCheck.position, boxRadiuses, Quaternion.identity, boxMask).Length > 0;
    }

    #endregion

    #region Movement

    public void ReceiveMovementInput(float movX, float movY)
    {
        movH = movX;
        movV = movY;
    }
    
    void HandleMovement()
    {
        // no mid-air control
        if (!isGrounded)
            return;
        
        if (!photonView.IsMine)
        {
            return;
        }


        Vector3 mov = transform.forward * movV + transform.right * movH;
        mov.Normalize();
        mov *= speed;
        SetAnimatorSpeed(mov);
        
        mov += new Vector3(0, rb.velocity.y, 0);
        rb.velocity = mov;
    }

    public void SetAnimatorSpeed(Vector3 horizontalVelocity)
    {
        animator.SetFloat(Speed, horizontalVelocity.magnitude);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        var position = GroundCheck.position;
        Gizmos.DrawSphere(position, 0.05f);
        Gizmos.DrawWireCube(position, boxRadiuses * 2);
    }

    #endregion
}