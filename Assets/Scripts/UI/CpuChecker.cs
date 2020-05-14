using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
        private void Update()
        {
            cpu = GetCurrentCpuUsage();
            
            
        }

        private string GetCurrentCpuUsage()
        {
            var temp = cpuCounter.NextValue();
            return temp +"%";
        }
    }
}
