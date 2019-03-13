using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float run;
    public float jump;
    public Text timerText;
    public float timer;
    public float gravity;
    public CharacterController control;
    private Vector3 move;
    public Animator animate;
    public Text hwText;
    private int hw = 5;
    public Text healthText;
    private int health = 100;
    public GameObject part;


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
        //timer
        if(timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F"); 
        }
        //animations
        if (Input.GetKey(KeyCode.W))
        {
            animate.SetBool("idle", false);
            animate.SetBool("is_walking", true);
            animate.SetBool("Walkingf", true);
        
        }
        //walk back
        else if (Input.GetKey(KeyCode.S))
        {
            animate.SetBool("is_walking", true);
            animate.SetBool("Walkingb", true);
            animate.SetBool("idle", false);
        }
        else
        {
            animate.SetBool("is_walking", false);
            animate.SetBool("is_walking", false);
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hw"))
        {
            other.gameObject.SetActive(false);
            hw--;
            hwText.text = "HW Left: " + hw.ToString();
        }

        if (other.gameObject.CompareTag("enemy"))
        {
            //other.gameObject.SetActive(false);
            health = health - 10;
            healthText.text = "Health: " + health.ToString();
        }
        
       

        
    }
}
