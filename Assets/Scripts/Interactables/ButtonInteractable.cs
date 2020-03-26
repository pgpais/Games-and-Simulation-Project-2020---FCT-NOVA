using Activatables;
using UnityEngine;

namespace Interactables
{
    public class ButtonInteractable : MonoBehaviour, Interactable
    {

        [Tooltip("Object this button will activate")]
        public GameObject objectToActivate;

        private Activatable activatable;
        
        // Start is called before the first frame update
        void Start()
        {
            activatable = objectToActivate.GetComponent<Activatable>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Interact()
        {
            Debug.Log("Button activated", gameObject);
            activatable.Activate();
        }
    }
}
