using System;
using UnityEngine;
using UnityEngine.Profiling;

namespace UI
{
    public class MemoryChecker : MonoBehaviour
    {
        public double AllocatedMemory { get; private set; }
        public long AllocatedGpu { get; private set; }
        
        public long NumberOfVertexes { get; private set; }
        
        public long NumberOfTriangles { get; private set; }
        

        // Update is called once per frame
        private void Update()
        {
            AllocatedMemory = Profiler.GetTotalAllocatedMemoryLong();

            AllocatedMemory /= 1024 * 1024;
            AllocatedMemory = Math.Truncate(AllocatedMemory * 100)/100;
            AllocatedGpu = Profiler.GetAllocatedMemoryForGraphicsDriver()/(1024*1024);
            
            
            CalculateTriangles();
        }

        private void CalculateTriangles()
        {
            NumberOfTriangles = 0;
            NumberOfVertexes = 0;
            foreach (var mf in FindObjectsOfType<MeshFilter>())
            {
                var sharedMesh = mf.sharedMesh;
                NumberOfVertexes += sharedMesh.vertexCount;
                NumberOfTriangles += sharedMesh.triangles.Length / 3;
            }
        }
    }
}
