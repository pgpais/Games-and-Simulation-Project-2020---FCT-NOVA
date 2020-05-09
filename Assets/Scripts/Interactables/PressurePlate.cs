using UnityEngine;

namespace Interactables
{
    public class PressurePlate : Switchable
    {
        private bool open = false;
        
        public bool allowPlayer = false;

        public bool colorChange = false;
        
        private void OnTriggerEnter(Collider other)
        {
            
            Debug.Log("I'm in");
            
            if (!open)
            {
                if (other.CompareTag("Carryable") || allowPlayer)
                {
                    open = true;
                    switchOn.Invoke();
                    //door.transform.position += new Vector3(0, height, 0);
                
                    if(colorChange)
                        GetComponent<Renderer>().material = Resources.Load("Materials/GreenMat", typeof(Material)) as Material;;

                }
                else
                {
                    if(colorChange)

                        GetComponent<Renderer>().material = Resources.Load("Materials/RedMat", typeof(Material)) as Material;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("I'm out");
            if (open)
            {
                switchOff.Invoke();
                //door.transform.position += new Vector3(0, -height, 0);
                open = false;

            }
            if(colorChange)
                GetComponent<Renderer>().material = Resources.Load("Materials/Grey", typeof(Material)) as Material;;
        }
    }
}