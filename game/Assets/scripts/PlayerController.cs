using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float run;
    public float jump;
    public float health;
    public float timer;
    public float gravity;
    public CharacterController control;
    private Vector3 move;
    public Animator animate;

    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<CharacterController>();
        animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // movement
        float yStore = move.y;
        move = (transform.forward * Input.GetAxis("Vertical")) +
            (transform.right * Input.GetAxis("Horizontal"));
        move = move.normalized * speed;
        move.y = yStore;
        // run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            move = (transform.forward * Input.GetAxis("Vertical")) +
            (transform.right * Input.GetAxis("Horizontal"));
            move = move.normalized * run;
            move.y = yStore;
        }
        // jump
        if (control.isGrounded)
        {
            move.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                move.y = jump;
            }
        }

        //animations
        if (Input.GetKey(KeyCode.W))
        {
            animate.SetBool("idle", false);
            animate.SetBool("walkf", true);
        }
        //walk back
        else if (Input.GetKey(KeyCode.S))
        {
            animate.SetBool("walkb", true);
            animate.SetBool("idle", false);
        }
        else
        {
            animate.SetBool("walkf", false);
            animate.SetBool("walkb", false);
            animate.SetBool("runf", false);
            animate.SetBool("runb", false);
            animate.SetBool("idle", true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animate.SetBool("idle", false);
            animate.SetBool("jumping", true);
        }
        move.y = move.y + (Physics.gravity.y * gravity * Time.deltaTime);
        control.Move(move * Time.deltaTime);
    }
}
