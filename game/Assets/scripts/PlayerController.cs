using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float run;
    public float jump;
    public Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        body.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, 
            body.velocity.y, Input.GetAxis("Vertical") * speed);
        // run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            body.velocity = new Vector3(Input.GetAxis("Horizontal") * run,
            body.velocity.y, Input.GetAxis("Vertical") * run);
        }
        // jump
        if (Input.GetButtonDown("Jump"))
        {
            body.velocity = new Vector3(body.velocity.x, jump, body.velocity.z);
        }
    }
}
