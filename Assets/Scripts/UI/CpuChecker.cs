using System.Diagnostics;
using UnityEngine;

namespace UI
{
    public class CpuChecker : MonoBehaviour
    {
        PerformanceCounter cpuCounter;

        public string cpu;

        // Start is called before the first frame update
        void Start()
        {
            cpuCounter = new PerformanceCounter
            {
                CategoryName = "Processor", CounterName = "% Processor Time", InstanceName = "_Total"
            };

        }

        // Update is called once per frame
        void Update()
        {
            print(GetCurrentCpuUsage());
            //cpuText.text = cpuCounter;
            cpu = "CPU: " + GetCurrentCpuUsage ();
            
            
        }

        private string GetCurrentCpuUsage(){
            return cpuCounter.NextValue()+"%";
        }
    }
}
