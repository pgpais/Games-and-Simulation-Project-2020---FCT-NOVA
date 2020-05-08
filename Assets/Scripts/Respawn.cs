using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform respawn;
   // [SerializeField] private Transform player;
    private void OnTriggerEnter(Collider other)
    {
            other.gameObject.transform.position = respawn.transform.position;
    }
}