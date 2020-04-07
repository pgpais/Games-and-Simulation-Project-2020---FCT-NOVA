using System;
using UnityEngine;

namespace Activatables
{
    /// <summary>
    /// Objects that can be activated.
    /// Meaning that they don't go back to a "deactivated" state. 
    /// </summary>
    public abstract class Activatable: Bolt.EntityBehaviour<IButtonState>
    {
        private BoltEntity boltEntity;

        public override void Attached()
        {
            
        }

        protected virtual void Start()
        {
            boltEntity = GetComponent<BoltEntity>();
            gameObject.tag = "Activatable";
        }

        public virtual void Activate()
        {
            var ev = ActivatedObject.Create();
            ev.Activatable = boltEntity;
        }
    }
}
