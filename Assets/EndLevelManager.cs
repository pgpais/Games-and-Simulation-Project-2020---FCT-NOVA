using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject floor;


    public void destroyFloor()
    {
        Destroy(floor);
    }
}
