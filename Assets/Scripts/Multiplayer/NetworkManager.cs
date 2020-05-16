using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = System.Random;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    
    private string gameVersion = "1";
    private string roomName;
    public string RoomName => roomName;

    public int Seed { get; private set; }
    private readonly byte SeedGeneratedEvent = 1;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        
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
            if (PhotonNetwork.CreateRoom("lobby", options))
                this.roomName = "lobby";
            else
            {
                this.roomName = null;
            }
        }
        else
        {
            Debug.Log("Trying to Host Lobby " + roomName);
            if (PhotonNetwork.CreateRoom(roomName, options))
                this.roomName = roomName;
            else
            {
                this.roomName = null;
            }
        }
        PhotonNetwork.LoadLevel(1);
    }
    
    public void JoinLobby(string roomName)
    {
        if (roomName.Equals(""))
        {
            Debug.Log("Trying to Join Lobby lobby");
            if(PhotonNetwork.JoinRoom("lobby"))
                this.roomName = "lobby";
            else
            {
                this.roomName = null;
            }
        }
        else
        {
            Debug.Log("Trying to Join Lobby " + roomName);
            if (PhotonNetwork.JoinRoom(roomName))
                this.roomName = roomName;
            else
            {
                this.roomName = null;
            }

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

    public void triggerReadyRPC(string readyFunc, Player player)
    {
        photonView.RPC(readyFunc, RpcTarget.All, player);
    }
    
    [PunRPC]
    public void PlayerReadied(Player player)
    {
        LobbyManager.instance.ReadyPlayer();
    }
    
    [PunRPC]
    public void PlayerUnreadied(Player player)
    {
        LobbyManager.instance.UnreadyPlayer();
    }

    #endregion

    #region In-Game

    public void LaunchSpawnPlayers()
    {
        photonView.RPC("SpawnPlayers", RpcTarget.All);
        PickSeed();
    }

    void PickSeed()
    {
        if (PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnected)
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
            SendOptions sendOptions = new SendOptions { Reliability = true };
            int seed = new Random().Next();
            Debug.Log("Raising event SeedGenerated");
            PhotonNetwork.RaiseEvent(SeedGeneratedEvent, seed, raiseEventOptions,
                sendOptions);
        }
    }
    void OnEvent(EventData data)
    {
        byte eventCode = data.Code;

        switch (eventCode)
        {
            case 1:
                Debug.Log("Setting seed to " + (int) data.CustomData);
                Seed = (int)data.CustomData;
                Debug.Log("Seed = " + Seed);
                break;
        }
    }
    
    [PunRPC]
    void SpawnPlayers()
    {
        GameManager.instance.SpawnPlayers();
    }

    #endregion
}
