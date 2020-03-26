using UnityEngine;

namespace Activatables
{
    public class SlidingWall : Activatable
    {

        public override void Activate()
        {
            Destroy(gameObject);
        }
    }
}
