using System;
using System.Collections;
using System.Collections.Generic;
using Bolt.Matchmaking;
using UdpKit;
using UnityEngine;

public class Menu : Bolt.GlobalEventListener
{
    public void StartServer()
    {
        BoltLauncher.StartServer();
    }

    public void StartClient()
    {
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone()
    {
        if (!BoltNetwork.IsServer) return;
        const string matchName = "test";
        BoltMatchmaking.CreateSession(matchName, null, "Intro Level - MP");
    }

    //loops through the servers to get them
    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value;

            if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltNetwork.Connect(photonSession);
            }
            
        }
    }
}
