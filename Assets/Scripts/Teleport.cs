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
    [SerializeField] private Transform clientTeleportPoint;
    //[SerializeField] private Transform player;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Carryable"))
            other.gameObject.transform.position = masterTeleportPoint.position;
        
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = PhotonNetwork.IsMasterClient? masterTeleportPoint.position: clientTeleportPoint.position;
            if (GameManager.instance != null && PhotonView.Get(other.gameObject).IsMine && bluePortal)
            {
                //GameManager.instance.SpawnPuzzleRooms(1);
                //GameManager.instance.DeletePuzzle();
                //GameManager.instance.UpdateTeleport();
            }
            PlayPortalSound();
        }
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