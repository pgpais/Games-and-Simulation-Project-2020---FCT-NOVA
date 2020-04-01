using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public Transform player;

    public Transform playerCamera;

    public float force = 5;

    private bool beingCarried = false;

    private bool closePlayer = false;

    public int damage;


    // Update is called once per frame
    void Update()
    {
        var distanceFromPlayer = Vector3.Distance(gameObject.transform.position, player.position);

        if (distanceFromPlayer <= 2.5)
        {
            closePlayer = true;
        }

        if (closePlayer && Input.GetButtonDown("Use"))
        {

            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCamera;
            beingCarried = true;

        }

        if (beingCarried)
        {
            if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCamera.forward * force);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
            }
        }

    }
}