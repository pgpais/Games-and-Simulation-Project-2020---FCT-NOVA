using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class LadderClimb : MonoBehaviour {
 
    private GameObject player;
    [SerializeField]bool canClimb = false;
    [SerializeField]float speed = 1;
 
    void OnCollisionEnter(Collision coll)
    {
        if (!coll.gameObject.CompareTag("Player")) return;
        canClimb = true;
        player = coll.gameObject;
    }
 
    void OnCollisionExit(Collision coll2)
    {
        if (!coll2.gameObject.CompareTag("Player")) return;
        canClimb = false;
        player = null;
    }
    void Update()
    {
        if (!canClimb) return;
        if (Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(new Vector3(0, 1, 0) * (Time.deltaTime * speed));
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.Translate(new Vector3(0, -1, 0) * (Time.deltaTime * speed));
        }
    }
}