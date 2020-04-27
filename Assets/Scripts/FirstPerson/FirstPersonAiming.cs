using System.Collections;
using System.Collections.Generic;
using Interactables;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class FirstPersonAiming : MonoBehaviour
{
    [Header("Controls Parameters")] public float sensitivity = 0.1f;

    private float aimX, aimY;
    private float rotY;

    [Header("Interact Parameters")] [SerializeField]
    private float interactRange = 10f;

    [SerializeField] private LayerMask interactMask;


    [Header("Camera Parameters")] [Range(0f, 90f)]
    public float maxClamp = 90.0f;
    private float minClamp;

    private Camera cam;
    [SerializeField] private Transform camTransform;
    private float rotX;

    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;

        cam = GetComponentInChildren<Camera>();
        camTransform = cam.transform;
        minClamp = -maxClamp;
        
        rotX = 0;
        rotY = 0;
    }


    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        //TryInteracting();
    }

    // TODO: can this be done as soon as input is received?
    public void ReceiveAimInput(float mouseX, float mouseY)
    {
        aimX = mouseX;
        aimY = mouseY;
    }
    
    void HandleAiming()
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
    public void TryInteracting()
    {
        RaycastHit hit;
        Debug.DrawRay(camTransform.position, camTransform.forward, Color.red, 3f);
        if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, interactRange, interactMask))
        {
            Debug.Log("Hit an object " + hit.collider.name, this);
            if (hit.collider.CompareTag("Interactable"))
            {
                // TODO: This but with better performance? SendMessage?
                
                hit.collider.GetComponentInParent<Interactable>().Interact();
            }

            if (hit.collider.CompareTag("Carryable"))
            {
                
            }
        }
    }
    
    #region Helpers

    public void DisableCamera()
    {
        cam.enabled = false;
    }
    #endregion
}