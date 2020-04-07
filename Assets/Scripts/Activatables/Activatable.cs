using System;
using UnityEngine;

namespace Activatables
{
    /// <summary>
    /// Objects that can be activated.
    /// Meaning that they don't go back to a "deactivated" state. 
    /// </summary>
    public abstract class Activatable: MonoBehaviour
    {
        protected virtual void Start()
        {
            gameObject.tag = "Activatable";
        }

        public virtual void Activate()
        {
            var ev = ActivatedObject.Create();
        }
    }
}
