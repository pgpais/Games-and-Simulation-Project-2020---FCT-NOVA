using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    
    public Text textLog;

    [SerializeField]
    private GameObject readyButton;
    [SerializeField]
    private GameObject unreadyButton;

    [SerializeField] private TMP_Text lobbyName;

    private List<string> log;

    private int readyNum = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;

        log = new List<string>();
        lobbyName.text = NetworkManager.instance.RoomName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateLog(string newLine)
    {
        log.Add(newLine);
        
        string l = "";
        foreach (string line in log)
        {
            l += line + "\n";
        }

        textLog.text = l;
    }
    
    public void LeaveLobby()
    {
        NetworkManager.instance.LeaveLobby();
    }

    public void OnReadyPressed()
    {
        readyButton.SetActive(false);
        unreadyButton.SetActive(true);
        NetworkManager.instance.triggerReadyRPC("PlayerReadied", PhotonNetwork.LocalPlayer);
    }
    public void OnUnreadyPressed()
    {
        readyButton.SetActive(true);
        unreadyButton.SetActive(false);
        NetworkManager.instance.triggerReadyRPC("PlayerUnreadied", PhotonNetwork.LocalPlayer);
    }
    
    public void ReadyPlayer()
    {
        readyNum++;
        updateLog("Player " + PhotonNetwork.LocalPlayer.NickName + " readied up!");
        if (readyNum == 2)
        {
            updateLog("All players are ready!");
            if (PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(StartGameCountdown(5)); // Start game in 5 seconds
            }
        }

    }

    public void UnreadyPlayer()
    {
        readyNum--;
        updateLog("Player " + PhotonNetwork.LocalPlayer.NickName + " disabled ready!");
        if (readyNum == 1)
        {
            updateLog("Players no longer ready!");
        }
    }

    IEnumerator StartGameCountdown(int timeToStart)
    {
        while (true)
        {
            if (readyNum < 2)
            {
                updateLog("Game start cancelled!");
                break;
            }
            if (timeToStart == 0)
            {
                //Start Game
                StartGame();
                Debug.Log("Game has started!");
                break;
            }
            else
            {
                updateLog("Game starting in " + timeToStart + " seconds!");
                timeToStart--;
                yield return new WaitForSeconds(1);
            }
        }
    }

    void StartGame()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
