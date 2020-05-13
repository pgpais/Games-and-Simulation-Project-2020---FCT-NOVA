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
    
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

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
            
            var position = cube.transform.position;
            var newPosition = new Vector3(position.x, position.y +  height, position.z);
            
            cube.transform.position = Vector3.SmoothDamp(position, newPosition, ref velocity, smoothTime, Mathf.Infinity,  deltaTime: Time.deltaTime);
            
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
