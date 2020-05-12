using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform checkpoint;
    [SerializeField] private Transform box;
   // [SerializeField] private Transform player;
    private void OnTriggerEnter(Collider other)
    {
        box.position = checkpoint.position;
    }

}