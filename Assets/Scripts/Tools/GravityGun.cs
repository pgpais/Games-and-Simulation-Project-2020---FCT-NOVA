using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GravityGun : Tool
{
    [Header("Gravity Gun Parameters")]
    
    [Tooltip("The radius of the gravity gun effect")][Range(1f,10f)]
    public float effectRadius = 1f;

    [Tooltip("The distance of the gravity gun effect")][Range(10f, 100f)]
    public float effectDistance = 10f;

    [Tooltip("The distance from which the gun grabs the object instead of pulling")]
    public float grabbingDistance = 3f;

    [Tooltip("The force applied by the gun")][Range(10f, 1000f)]
    public float force = 10f;

    [Tooltip("Force applied when launching object with gun")]
    public float launchForce = 100f;
    
    public Transform gravityGunTransform;
    [SerializeField] private Transform grabbingPoint;
    [SerializeField] private Camera cam;

    private bool isActive;
    private bool obj;
    private Rigidbody objRB;
    private bool isGrabbing;

    // Start is called before the first frame update
    void Start()
    {
        gravityGunTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isActive)
            pullObject();
    }

    private void pullObject()
    {
        RaycastHit hit;

        if (Physics.SphereCast(cam.transform.position, effectRadius, cam.transform.forward, out hit,
            effectDistance))
        {
            //Debug.Log("Hit an object " + hit.collider.name, this);

            if (hit.collider.CompareTag("Carryable"))
            {
                if (!obj) // save object rb if we don't have it yet
                {
                    objRB = hit.collider.GetComponent<Rigidbody>();
                    obj = true;
                }
                
                if (hit.distance < grabbingDistance)
                {
                    Grab();
                }
                else
                {
                    Debug.Log("Pulling object");
                    objRB.AddForce((cam.transform.position - objRB.position).normalized * force);
                }

            }
            
        }
    }

    public override void UseTool(InputActionPhase phase)
    {
        switch (phase)
        {
            case InputActionPhase.Performed:
                if(isGrabbing)
                {
                    Drop();
                }
                else
                {
                    isActive = true;
                }
                break;
            case InputActionPhase.Canceled:
                isActive = false;
                break;
        }
    }

    public override void UseToolSecondary(InputActionPhase phase)
    {
        if (phase == InputActionPhase.Performed)
        {
            if (isGrabbing)
            {
                LaunchObject();
            }
            else
            {
                Debug.Log("Tried Launching but nope");
            }
        }
    }

    private void LaunchObject()
    {
        Debug.Log("Launched Object");
        objRB.isKinematic = false;
        objRB.transform.parent = null;
        objRB.AddForce(cam.transform.forward * launchForce, ForceMode.Force);
        Drop();
    }

    private void Grab()
    {
        Debug.Log("GRabbed");
        objRB.isKinematic = true;
        
        var transform1 = objRB.transform;
        transform1.parent = grabbingPoint;
        transform1.localPosition = Vector3.zero;
        
        isGrabbing = true;
    }

    private void Drop()
    {
        Debug.Log("Dropped");
        objRB.isKinematic = false;
        objRB.transform.parent = null;
        
        obj = false;
        objRB = null;
        
        isGrabbing = false;
    }
}
