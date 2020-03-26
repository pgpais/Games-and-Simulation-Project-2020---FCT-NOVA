using UnityEngine;

namespace Activatables
{
    public class SlidingWall : MonoBehaviour, Activatable
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Activate()
        {
            Destroy(gameObject);
        }
    }
}
