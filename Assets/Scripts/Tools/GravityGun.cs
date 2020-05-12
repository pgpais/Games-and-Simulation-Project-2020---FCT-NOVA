using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravityGun : Tool
{
    [Header("Gravity Gun Parameters")]
    
    [Tooltip("The radius of the gravity gun effect")][Range(1f,10f)]
    public float effectRadius = 1f;

    [Tooltip("The distance of the gravity gun effect")][Range(10f, 100f)]
    public float effectDistance = 10f;

    [Tooltip("The force applied by the gun")][Range(10f, 300f)]
    public float force = 10f;
    
    public Transform gravityGunTransform;

    private bool isActive;
    
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
            PushObject();
    }

    private void PushObject()
    {
        RaycastHit hit;

        if (Physics.SphereCast(Camera.main.transform.position, effectRadius, Camera.main.transform.forward, out hit,
            effectDistance))
        {
            //Debug.Log("Hit an object " + hit.collider.name, this);

            if (hit.collider.CompareTag("Carryable"))
            {
                if (hit.distance < 1f)
                { 
                    //Grab();   
                }
                else
                {
                    Rigidbody obj = hit.collider.GetComponent<Rigidbody>();
                    obj.AddForce((Camera.main.transform.position - obj.position).normalized * force );
                }

            }
            
        }
    }

    public override void UseTool(InputActionPhase phase)
    {
        switch (phase)
        {
            case InputActionPhase.Performed:
                isActive = true;
                break;
            case InputActionPhase.Canceled:
                isActive = false;
                break;
        }
    }
}
