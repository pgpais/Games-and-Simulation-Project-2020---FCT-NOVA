using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEditor;
using UnityEngine;

public class FirstPersonAiming : Bolt.EntityBehaviour<ICustomPlayerState>
{
    [Header("Controls Parameters")]
    public float sensitivity = 10f;

    private float rotY;

    [Header("Interact Parameters")] 
    [SerializeField]
    private float interactRange = 10f;
    [SerializeField] private LayerMask interactMask;
    

    
    [Header("Camera Parameters")] 
    [Range(0f, 90f)]
    public float maxClamp = 90.0f;
    private float minClamp;
    
    private Camera cam;
    [SerializeField]
    private Transform camTransform;
    private float rotX;
    
    // Start is called before the first frame update
    void Start()
    {


        Cursor.lockState = CursorLockMode.Locked;

        cam = GetComponentInChildren<Camera>();
        camTransform = cam.transform;
        minClamp = -maxClamp;

        if (!entity.HasControl){
            cam.gameObject.SetActive(false);
        }


        
        rotX = 0;
        rotY = 0;
    }


    // Update is called once per frame
    void Update()
    {
        HandleAiming(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        if (Input.GetButtonDown("Use"))
        {
            TryInteracting();
        }
    }

    void HandleAiming(float aimX, float aimY)
    {
        // Add rotations
        rotX -= aimY * sensitivity;
        rotY += aimX * sensitivity;

        // Clamp camera rotation so the player doesn't turn upside down
        rotX = Mathf.Clamp(rotX, minClamp, maxClamp);
        
        // Set rotations
        Vector3 bodyRotation = transform.eulerAngles;
        Vector3 camRotation = camTransform.localRotation.eulerAngles;
        bodyRotation.y = rotY;
        camRotation.x = rotX;
        camTransform.localRotation = Quaternion.Euler(camRotation);
        transform.eulerAngles = bodyRotation;
    }

    /// <summary>
    /// Shoots a Raycast forward to look for an Interactable
    /// </summary>
    void TryInteracting()
    {
        RaycastHit hit;
        Debug.DrawRay(camTransform.position, camTransform.forward, Color.red, 3f);
        if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, interactRange, interactMask))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                // TODO: This but with better performance? SendMessage?
                hit.collider.GetComponent<Interactable>().Interact();
            }
        }
    }
}