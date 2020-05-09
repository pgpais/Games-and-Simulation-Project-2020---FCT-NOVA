using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

public class SlidingCubesPuzzle : MonoBehaviour
{
    [SerializeField]
    private List<Transform> cubes;

    [SerializeField]
    private float height;

    [SerializeField]
    private float teleporterHeight;

    [SerializeField] private Transform exitTeleporter;

    public void showTeleporter()
    {
        exitTeleporter.transform.position += new Vector3(0, teleporterHeight, 0);
    }
    
    public void hideTeleporter()
    {
        exitTeleporter.transform.position -= new Vector3(0, teleporterHeight, 0);
    }
    

    public void moveCubesUp()
    {
        foreach (var cube in cubes)
        {
            
            cube.transform.position += new Vector3(0, height, 0);
        }
    }
    
    public void moveCubesDown()
    {
        foreach (var cube in cubes)
        {
            
            cube.transform.position -= new Vector3(0, height, 0);
        }
    }
}
