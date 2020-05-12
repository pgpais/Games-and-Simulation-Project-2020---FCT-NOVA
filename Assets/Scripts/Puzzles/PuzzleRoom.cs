using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PuzzleRoom : MonoBehaviour
{
    [SerializeField] private Transform clientSpawnPoint;
    public Transform ClientSpawnPoint => clientSpawnPoint;

    [SerializeField] private Transform masterSpawnPoint;
    public Transform MasterSpawnPoint => masterSpawnPoint;

    [SerializeField] private Teleport teleport;
    public Teleport Teleport => teleport;

    [SerializeField] private List<GameObject> objectsToSpawn;
    public List<GameObject> ObjectsToSpawn => objectsToSpawn;
    [SerializeField] private List<Transform> whereToSpawn;
    public List<Transform> WhereToSpawn => whereToSpawn;

    private void Start()
    {
        if (objectsToSpawn.Count != whereToSpawn.Count)
        {
            Debug.LogException(new Exception("Objects and positions with different length, dummies"));
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        if(PhotonNetwork.IsMasterClient)
            for (int i = 0; i < objectsToSpawn.Count; i++)
            {
                PhotonNetwork.Instantiate(objectsToSpawn[i].name, whereToSpawn[i].position, whereToSpawn[i].rotation);
            }
    }


    public void InstantiatePuzzleSphere(Transform pos)
    {
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate("Sphere", pos.position, pos.rotation);
    }
    
    public void InstantiatePuzzleBox(Transform pos)
    {
        if(PhotonNetwork.IsMasterClient) 
            PhotonNetwork.Instantiate("Box", pos.position, pos.rotation);
    }
}
