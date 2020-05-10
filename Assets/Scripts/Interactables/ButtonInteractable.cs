using Activatables;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

namespace Interactables
{
    [RequireComponent(typeof(Animator))]
    public class ButtonInteractable : Interactable
    {
        //TODO: check photon bolt events
        [Tooltip("Object this button will activate")]
        public Activatable activatable;

        public UnityEvent activateButton;

        private Animator anim;
        private static readonly int Activated = Animator.StringToHash("Activated");

        protected override void Start()
        {
            base.Start();
            anim = GetComponent<Animator>();
        }

        public override void Interact()
        {
            PhotonView.Get(this).RPC("RpcInteract", RpcTarget.All);
        }
        
        [PunRPC]
        private void RpcInteract()
        {
            Debug.Log("Button activated", gameObject);
            anim.SetTrigger(Activated);
            //activatable.Activate();
            activateButton.Invoke();
        }
    }
}