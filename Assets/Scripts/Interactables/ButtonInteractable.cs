using Activatables;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(Animator))]
    public class ButtonInteractable : Interactable
    {
        //TODO: check photon bolt events
        [Tooltip("Object this button will activate")]
        public Activatable activatable;

        private Animator anim;
        private static readonly int Activated = Animator.StringToHash("Activated");

        protected override void Start()
        {
            base.Start();

            anim = GetComponent<Animator>();
        }

        public override void Interact()
        {
            Debug.Log("Button activated", gameObject);
            anim.SetTrigger(Activated);
            activatable.Activate();
            ActivatedObject ev = ActivatedObject.Create();
            ev.Send();
            
        }
    }
}