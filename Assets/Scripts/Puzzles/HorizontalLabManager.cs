using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

public class HorizontalLabManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ceiling;
    


    public void destroyWall()
    {
        Destroy(ceiling);
    }
    
    
}