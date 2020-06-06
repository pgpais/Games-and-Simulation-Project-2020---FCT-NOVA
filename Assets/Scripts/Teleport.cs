using System;
using System.Collections;
using System.Collections.Generic;
using FirstPerson;
using Photon.Pun;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public bool bluePortal = true;
    public AudioClip portalSound;
    [SerializeField] private Transform masterTeleportPoint;
    public Transform MasterTeleportPoint => masterTeleportPoint;
    [SerializeField] private Transform clientTeleportPoint;

    public Transform ClientTeleportPoint => clientTeleportPoint;
    //[SerializeField] private Transform player;
   
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Carryable"))
            other.gameObject.transform.position = masterTeleportPoint.position;
    }

    public void PlayPortalSound()
    {
        FirstPersonPlayer.LocalPlayerInstance.Play(portalSound);
    }
    
    public void SetPoints(Transform masterSpawnPoint, Transform clientSpawnPoint)
    {
        masterTeleportPoint = masterSpawnPoint;
        clientTeleportPoint = clientSpawnPoint;
    }
}