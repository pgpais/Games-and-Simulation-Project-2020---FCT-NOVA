using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMeshGenerator : MonoBehaviour
{
    public int xSize = 20, zSize = 20, ySize = 1; // ySize = nSegments?
    public int nSegments;
    
    
    private Mesh mesh;
    
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        
        mesh.RecalculateNormals();
    }
    
    private void CreateShape()
    {
        // create vertices
        vertices = new Vector3[(xSize+1) * (zSize+1) * (ySize+1)];

        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int z = 0; z <= zSize; z++)
            {
                for (int x = 0; x <= xSize; x++)
                {
                    //float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 5f;
                    vertices[i] = new Vector3(x, -y, z);
                    i++;
                }
            }
        }

        
        // create triangles
        triangles = new int[xSize * zSize * ySize * 6];
        
        int vert = 0;
        int tris = 0;
        for (int y = 0; y < ySize; y++)
        {
            for (int z = 0; z < zSize; z++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    triangles[tris] = vert + 0;
                    triangles[tris + 1] = vert + xSize + 1;
                    triangles[tris + 2] = vert + 1;
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + xSize + 1;
                    triangles[tris + 5] = vert + xSize + 2;

                    vert++;
                    tris += 6;
                }

                vert++;
            }
        }

        uvs = new Vector2[vertices.Length];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2((float)x / xSize, (float)z/zSize);
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnDrawGizmosSelected()
    {
        if(vertices == null)
            return;

        foreach (var vertex in vertices)
        {
            Gizmos.DrawSphere(transform.position + vertex, .1f);
        }
    }
}
