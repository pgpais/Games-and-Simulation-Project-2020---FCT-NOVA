using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBallToRamp : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private Transform ballDropper;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropBall()
    {
        ball.SetActive(true);
        ball.transform.position = ballDropper.position;
    }
}
