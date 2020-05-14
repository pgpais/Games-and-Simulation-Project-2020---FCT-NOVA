using System.Collections;
using System.Collections.Generic;
using Interactables;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class FirstPersonAiming : MonoBehaviourPun
{
    [Header("Controls Parameters")] public float sensitivity = 0.1f;

    private float aimX, aimY;
    private float rotY;



    [Header("Camera Parameters")] [Range(0f, 90f)]
    public float maxClamp = 90.0f;
    private float minClamp;

    private Camera cam;
    [SerializeField] private Transform camTrans;

    public Transform camTransform => camTrans;
    
    private float rotX;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam = GetComponentInChildren<Camera>();
        camTrans = cam.transform;
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
        if (!photonView.IsMine)
        {
            return;
        }
        
        // Add rotations
        rotX -= aimY * sensitivity;
        rotY += aimX * sensitivity;

        // Clamp camera rotation so the player doesn't turn upside down
        rotX = Mathf.Clamp(rotX, minClamp, maxClamp);

        // Set rotations
        Vector3 bodyRotation = transform.eulerAngles;
        Vector3 camRotation = camTrans.localRotation.eulerAngles;
        bodyRotation.y = rotY;
        camRotation.x = rotX;
        camTrans.localRotation = Quaternion.Euler(camRotation);
        transform.eulerAngles = bodyRotation;
    }

    
    
    #region Helpers

    public void EnableAiming()
    {
        
    }

    public void DisableAiming()
    {
        
    }
    
    public void DisableCamera()
    {
        cam.enabled = false;
        cam.GetComponent<AudioListener>().enabled = false;
    }
    #endregion
}