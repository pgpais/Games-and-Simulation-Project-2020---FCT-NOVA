using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public int length, width;
    public int edgeLength;
    
    private Mesh mesh;
    
    private Vector3[] vertices;
    private int[] triangles;

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
        
        mesh.RecalculateNormals();
    }

    private void CreateShape()
    {
        vertices = new Vector3[length * width];
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                vertices[i] = new Vector3(edgeLength*i, 0, edgeLength * j);
            }
        }
        
        triangles = new int[]
        {
            0, 1, 2,
            1, 3, 2
        };
    }
}
