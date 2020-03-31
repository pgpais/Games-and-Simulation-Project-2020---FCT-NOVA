using UnityEngine;

//detect if network has been instantianted if not it'll do it automatically and on disconnect it'll destroy it
[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene)
    {
        if (BoltNetwork.IsServer)
                        BoltNetwork.Instantiate(BoltPrefabs.FirstPersonPlayer,
                             new Vector3(-9, 1, -1) , Quaternion.identity).TakeControl();

        else
        {
            BoltNetwork.Instantiate(BoltPrefabs.FirstPersonPlayer,
                new Vector3(-3, 1, -1) , Quaternion.identity).TakeControl();
        }
    }
}
 