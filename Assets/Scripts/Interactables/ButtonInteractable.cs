using Activatables;
using UnityEngine;

namespace Interactables
{
    public class ButtonInteractable : Interactable
    {

        [Tooltip("Object this button will activate")]
        public Activatable activatable;
        
        public override void Interact()
        {
            Debug.Log("Button activated", gameObject);
            activatable.Activate();
        }
    }
}
