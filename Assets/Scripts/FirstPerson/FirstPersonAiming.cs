using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FirstPersonAiming : MonoBehaviour
{
    public float sensitivity = 10f;

    private float rotY;

    [Header("Camera Properties")] 
    public float maxClamp = 90.0f;
    public float minClamp = -90.0f;
    private Camera cam;
    [SerializeField]
    private Transform camTransform;
    private float rotX;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = camTransform.GetComponent<Camera>();
        rotX = 0;
        rotY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
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
}
