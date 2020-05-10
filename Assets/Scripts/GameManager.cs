using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private List<GameObject> puzzles; // List of all puzzles
    private int nextPuzzleToSpawnIndex;
    private List<GameObject> spawnedPuzzles; // List of spawned and active;
    
    private PuzzleRoom nextPuzzle; // Script of next puzzle
    
    [SerializeField] 
    private List<Transform> puzzleSpawnList; 
    private int nextSpawnIndex = 0;
    
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
            Debug.LogError("OFFLINE MODE");
            LocalSpawnPlayers();
        }
        else
        {
            if(PhotonNetwork.IsMasterClient)
                NetworkManager.instance.LaunchSpawnPlayers();
        }

        spawnedPuzzles = new List<GameObject>(puzzleSpawnList.Count);
        SpawnPuzzleRooms(1);
    }

    private void LocalSpawnPlayers()
    {
        Instantiate(LocalPlayerPrefab, masterSpawnPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPuzzleRooms(int howMany)
    {
        for (int i = 0; i < howMany; i++)
        {
            GameObject nextPuzzleObj = Instantiate(puzzles[nextPuzzleToSpawnIndex], puzzleSpawnList[nextSpawnIndex].position, Quaternion.identity);
            nextPuzzle = nextPuzzleObj.GetComponent<PuzzleRoom>();
            spawnedPuzzles.Insert(nextSpawnIndex, nextPuzzleObj);
            nextPuzzleToSpawnIndex = ++nextPuzzleToSpawnIndex % puzzles.Count;
            nextSpawnIndex = ++nextSpawnIndex % puzzleSpawnList.Count;
            if (nextPuzzleToSpawnIndex == puzzles.Count)
            {
                Debug.LogWarning("All puzzles have been spawned");
                //TODO: Do something here. Game will soon end
            }
        }
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

    public void TeleportPlayer(Transform player)
    {
        player.position = PhotonNetwork.IsMasterClient? nextPuzzle.MasterSpawnPoint.position : nextPuzzle.ClientSpawnPoint.position;
    }
}
