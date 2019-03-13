using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemies : MonoBehaviour
{

    private float distance;


    Transform player;
    Vector3 randomDirection;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;	// Find player
    }

    void Start()
    {

        randomDirection = new Vector3(Random.Range(-359, 359), 0, Random.Range(-359, 359));

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;	// Find direction to player
        // Debug.Log(direction);
        direction.y = 0;	// Init Y axis to 0 to prevent floating enemies
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.3f);	// Rotate to player
        distance = Vector3.Distance(player.transform.position, transform.position);

        // If player is too close, keep distance
        if (distance < 7)
        {
            // Charge towards player
            transform.position += transform.forward * 5 * Time.deltaTime;
        }
        else
        {
            randomDirection = new Vector3(Random.Range(-359, 359), 0, Random.Range(-359, 359));
        }

    }

}

