using Photon.Pun;
using UnityEngine;

namespace Local
{
    public class LocalGameManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            PhotonNetwork.OfflineMode = true;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
