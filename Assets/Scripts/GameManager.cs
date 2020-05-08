using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject LocalPlayerPrefab;
    [SerializeField]
    private GameObject RemotePlayerPrefab;

    [SerializeField]
    private Transform masterSpawnPoint;
    [SerializeField]
    private Transform clientSpawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        
        if (NetworkManager.instance == null)
        {
            PhotonNetwork.OfflineMode = true;
        }
        else
        {
            if(PhotonNetwork.IsMasterClient)
                NetworkManager.instance.LaunchSpawnPlayers();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnPlayers()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master instantiated");
            PhotonNetwork.Instantiate(LocalPlayerPrefab.name, masterSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Client instantiated");
            PhotonNetwork.Instantiate(LocalPlayerPrefab.name, clientSpawnPoint.position, Quaternion.identity);;
        }
    }
}
