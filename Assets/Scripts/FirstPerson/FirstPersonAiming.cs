using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FirstPersonAiming : MonoBehaviour
{
    public float sensitivity = 10f;

    private Camera cam;
    [SerializeField]
    private Transform camTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = camTransform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float aimX = Input.GetAxisRaw("Mouse X");
        float aimY = Input.GetAxisRaw("Mouse Y");
        HandleAiming(aimX, aimY);
    }

    void HandleAiming(float aimX, float aimY)
    {
        transform.Rotate(Vector3.up, aimX*sensitivity);
        
        camTransform.Rotate(Vector3.left, aimY*sensitivity);
    }
}
