using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    
    private string gameVersion = "1";
    
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("Tried to create a new NetworkManager. Please be sure that there is only one in the scene");
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(gameObject); // This object should not disappear when loading a new scene
        
        instance = this;
        Connect();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Connection management

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
        MenuManager.instance.ActivateButtons();
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }

    #endregion
    

    

    #region Room Management

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void CreateLobby(string roomName)
    {
        RoomOptions options = new RoomOptions {MaxPlayers = 2};
        if (roomName.Equals(""))
        {
            Debug.Log("Trying to Host Lobby lobby");
            PhotonNetwork.CreateRoom("lobby", options);
        }
        else
        {
            Debug.Log("Trying to Host Lobby " + roomName);
            PhotonNetwork.CreateRoom(roomName, options);
        }
        PhotonNetwork.LoadLevel(1);
    }
    
    public void JoinLobby(string roomName)
    {
        if (roomName.Equals(""))
        {
            Debug.Log("Trying to Join Lobby lobby");
            PhotonNetwork.JoinRoom("lobby");
        }
        else
        {
            Debug.Log("Trying to Join Lobby " + roomName);
            PhotonNetwork.JoinRoom(roomName);
        }
    }
    
    public override void OnPlayerEnteredRoom(Player player)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", player.NickName); // not seen if you're the player connecting

        if (LobbyManager.instance == null)
        {
            Debug.LogError("Tried accessing the LobbyManager but it doesn't exist!", this);
        }
        else
        {
            LobbyManager.instance.updateLog("Player " + player.NickName + " has joined the room");
        }
        
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}",
                PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    public override void OnPlayerLeftRoom(Player player)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", player.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
        }
    }

    #endregion
}
