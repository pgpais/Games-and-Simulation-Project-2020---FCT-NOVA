using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

public class HorizontalLabManager : MonoBehaviour
{
    [SerializeField] private GameObject ceiling;
    [SerializeField] private GameObject teleport;


    public void destroyWall()
    {
        Destroy(ceiling);
    }

    public void spawnTeleport()
    {
        
        teleport.SetActive(true);
    }
    
    
}