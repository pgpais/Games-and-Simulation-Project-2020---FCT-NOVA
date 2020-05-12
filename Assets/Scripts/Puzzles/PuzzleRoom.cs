using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom : MonoBehaviour
{
    [SerializeField] private Transform clientSpawnPoint;
    public Transform ClientSpawnPoint => clientSpawnPoint;

    [SerializeField] private Transform masterSpawnPoint;
    public Transform MasterSpawnPoint => masterSpawnPoint;

    [SerializeField] private Teleport teleport;
    public Teleport Teleport => teleport;
}
