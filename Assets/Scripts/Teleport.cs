﻿using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform masterTeleportPoint;
    [SerializeField] private Transform clientTeleportPoint;
   // [SerializeField] private Transform player;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = masterTeleportPoint.position;
        
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = PhotonNetwork.IsMasterClient? masterTeleportPoint.position: clientTeleportPoint.position;
        }
    }

    public void SetPoints(Transform masterSpawnPoint, Transform clientSpawnPoint)
    {
        masterTeleportPoint = masterSpawnPoint;
        clientTeleportPoint = clientSpawnPoint;
    }
}