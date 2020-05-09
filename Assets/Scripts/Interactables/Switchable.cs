using UnityEngine;
using UnityEngine.Events;

namespace Interactables
{
    public class Switchable : MonoBehaviour
    {
        public UnityEvent switchOn;
        public UnityEvent switchOff;
        
        public virtual void SwitchOn()
        {
            switchOn.Invoke();
        }

        public virtual void SwitchOff()
        {
            switchOff.Invoke();
        }
    }
}