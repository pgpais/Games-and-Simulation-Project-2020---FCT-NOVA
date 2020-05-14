using Photon.Pun;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(PhotonView))]
    public abstract class Interactable : MonoBehaviour
    {
        protected virtual void Start()
        {
            gameObject.tag = "Interactable";
        }

        /// <summary>
        /// To be called by the player when trying to interact with an object
        /// </summary>
        public abstract void Interact();
    }
}