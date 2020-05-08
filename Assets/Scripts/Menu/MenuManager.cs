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
        NetworkManager.instance.CreateLobby(roomNameInput.text);
    }
    
    public void JoinLobby()
    {
        NetworkManager.instance.JoinLobby(roomNameInput.text);
    }

    public void ActivateButtons()
    {
        foreach (GameObject obj in objectsToActivateOnConnection)
        {
            obj.SetActive(true);
        }
    }
    
}
