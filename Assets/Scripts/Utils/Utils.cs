using UnityEngine;

namespace Utils
{
    public class Utils : MonoBehaviour
    {
        public static void SetLayerToChildren(GameObject o, int layer)
        {
            foreach (Transform t in o.GetComponentsInChildren<Transform>())
            {
                t.gameObject.layer = layer;
            }
        }
    }
}
