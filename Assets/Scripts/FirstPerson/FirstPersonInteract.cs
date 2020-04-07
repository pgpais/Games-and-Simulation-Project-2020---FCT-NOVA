using UnityEngine;

namespace FirstPerson
{
    public class FirstPersonInteract : MonoBehaviour
    {
        public Tool tool;

        void Update()
        {
            if (Input.GetButtonDown("UseTool"))
                tool.UseTool();
        }
    }
}