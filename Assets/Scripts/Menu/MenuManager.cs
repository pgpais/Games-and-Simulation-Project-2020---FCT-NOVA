using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField]
    private List<GameObject> objectsToActivateOnConnection;
    
    [SerializeField]
    private TMP_InputField roomNameInput;


    private void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("Tried to create a new MenuManager. Please be sure that there is only one in the scene");
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public void CreateLobby()
    {
        if (roomNameInput.text.Equals(""))
        {
            Debug.Log("Trying to Host Lobby lobby");
            //PhotonNetwork.CreateRoom("lobby"); //TODO: check if this works with nulls
        }
        else
        {
            Debug.Log("Trying to Host Lobby " + roomNameInput.text);
            //PhotonNetwork.CreateRoom(roomNameInput.text); //TODO: check if this works with nulls
        }
    }
    
    public void JoinLobby()
    {
        if (roomNameInput.text.Equals(""))
        {
            Debug.Log("Trying to Join Lobby lobby");
            //PhotonNetwork.JoinRoom("lobby"); //TODO: check if this works with nulls
        }
        else
        {
            Debug.Log("Trying to Join Lobby " + roomNameInput.text);
            //PhotonNetwork.JoinRoom(roomNameInput.text); //TODO: check if this works with nulls
        }
    }

    public void ActivateButtons()
    {
        foreach (GameObject obj in objectsToActivateOnConnection)
        {
            obj.SetActive(true);
        }
    }
    
}
