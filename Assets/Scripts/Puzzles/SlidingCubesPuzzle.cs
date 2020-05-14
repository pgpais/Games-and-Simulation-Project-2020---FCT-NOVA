using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlidingCubesPuzzle : MonoBehaviour
{
    [SerializeField]
    private List<Transform> cubes;

    private List<Vector3> cubesFinalPositionsUp;
    private List<Vector3> cubesFinalPositionsDown;
    private List<Vector3> cubesDefaultPositions;

    [SerializeField]
    private float height;

    [SerializeField]
    private float teleporterHeight;

    [SerializeField] private Transform exitTeleporter;
    
    [SerializeField]private float maxTimer;

    [SerializeField] private float steps;


    private void Start()
    {

        cubesDefaultPositions = new List<Vector3>(cubes.Count); 
        cubesFinalPositionsDown = new List<Vector3>(cubes.Count); 
        cubesFinalPositionsUp = new List<Vector3>(cubes.Count); 
        
        
        foreach (var position in cubes.Select(t => t.position))
        {
            cubesDefaultPositions.Add(position);
            var newPositionUp = new Vector3(position.x, position.y + height, position.z);
            var newPositionDown = new Vector3(position.x, position.y - height, position.z);
            cubesFinalPositionsUp.Add(newPositionUp);
            cubesFinalPositionsDown.Add(newPositionDown);
        }
    }
    
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
        for (var i = 0; i < cubes.Count; i++)
        {
            
            var position = cubes[i].transform.position;
            
            var newPosition = cubesFinalPositionsUp[i];

            StartCoroutine(MoveCubesUpHelper(cubes[i], position, newPosition, maxTimer, steps));
        }
    }

    private static IEnumerator MoveCubesUpHelper(Component cube, Vector3 startMarker, Vector3 endMarker, float time, float step)
    {
        var elapsedTime = 0f;

        while (elapsedTime <= time)
        {
            // Set our position as a fraction of the distance between the markers.
            cube.transform.position = Vector3.Lerp(startMarker, endMarker, Mathf.Min(elapsedTime / time, 1f));

            elapsedTime += step;

            yield return new WaitForSeconds(step);

        }

    }
    
    public void moveCubesDown()
    {
        for (var i = 0; i < cubes.Count; i++)
        {
            
            var position = cubes[i].transform.position;
            
            var newPosition = cubesFinalPositionsDown[i];

            StartCoroutine(MoveCubesDownHelper(cubes[i], position, newPosition, maxTimer, steps));
        }
    }
    
    private static IEnumerator MoveCubesDownHelper(Component cube, Vector3 startMarker, Vector3 endMarker, float time, float step)
    {
        var elapsedTime = 0f;

        while (elapsedTime <= time)
        {
            // Set our position as a fraction of the distance between the markers.
            cube.transform.position = Vector3.Lerp(startMarker, endMarker, Mathf.Min(elapsedTime / time, 1f));

            elapsedTime += step;

            yield return new WaitForSeconds(step);

        }

    }
    
    public void moveReset()
    {
        for (var i = 0; i < cubes.Count; i++)
        {
            
            var position = cubes[i].transform.position;
            
            var newPosition = cubesDefaultPositions[i];

            StartCoroutine(MoveCubesDefaultHelper(cubes[i], position, newPosition, maxTimer, steps));
        }
    }
    
    private static IEnumerator MoveCubesDefaultHelper(Component cube, Vector3 startMarker, Vector3 endMarker, float time, float step)
    {
        var elapsedTime = 0f;

        while (elapsedTime <= time)
        {
            // Set our position as a fraction of the distance between the markers.
            cube.transform.position = Vector3.Lerp(startMarker, endMarker, Mathf.Min(elapsedTime / time, 1f));

            elapsedTime += step;

            yield return new WaitForSeconds(step);

        }

    }
}
