using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    
    public Text textLog;
    
    //TODO: show lobby name
    private string lobbyName;

    private List<string> log;
    
    
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
}
