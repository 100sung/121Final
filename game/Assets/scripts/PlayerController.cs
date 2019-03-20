using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    //public Animator door;
    public Text hwText;
    private int hw = 5;
    public Text healthText;
    private int health = 100;
    public GameObject part;
    public AudioClip steps;
    public AudioClip dmg;
    public AudioClip pickup;
    public AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<CharacterController>();
        animate = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        //door.SetBool("open", true);
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


        //lose conditions
        if(timer <= 0.0f)
        {
            SceneManager.LoadScene("fail");
        }
        if(health <= 0)
        {
            SceneManager.LoadScene("fail");
        }
        //animations
        if (Input.GetKey(KeyCode.W))
        {
            
            animate.SetBool("is_walking", true);
            animate.SetBool("walkf", true);
            //animate.SetBool("grounded", control.isGrounded);
            step();
        
        }
        //walk back
        else if (Input.GetKey(KeyCode.S))
        {
            animate.SetBool("is_walking", true);
            animate.SetBool("walkb", true);
            animate.SetBool("jumping", false);
            animate.SetBool("walkf", false);
            animate.SetBool("walkl", false);
            animate.SetBool("walkr", false);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            animate.SetBool("is_walking", true);
            animate.SetBool("walkl", true);
            animate.SetBool("jumping", false);
            animate.SetBool("walkf", false);
            animate.SetBool("walkb", false);
            animate.SetBool("walkr", false);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            animate.SetBool("is_walking", true);
            animate.SetBool("walkr", true);
            animate.SetBool("jumping", false);
            animate.SetBool("walkf", false);
            animate.SetBool("walkl", false);
            animate.SetBool("walkb", false);

        }
        else
        {
            animate.SetBool("is_walking", false);
            animate.SetBool("walkf", false);
            animate.SetBool("walkl", false);
            animate.SetBool("walkb", false);
            animate.SetBool("walkr", false);
            animate.SetBool("idle", true);
            animate.SetBool("jumping", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && control.isGrounded)
        {
            animate.SetBool("idle", false);
            animate.SetBool("jumping", true);
            animate.SetBool("walkf", false);
            animate.SetBool("walkl", false);
            animate.SetBool("walkr", false);
            animate.SetBool("walkb", false);
        }
        else
        {
            animate.SetBool("jumping", false);
            if (Input.GetKey(KeyCode.W))
            {
                animate.SetBool("walkf", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                animate.SetBool("walkb", true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animate.SetBool("walkr", true);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                animate.SetBool("walkl", true);
            }
        }
        move.y = move.y + (Physics.gravity.y * gravity * Time.deltaTime);
        control.Move(move * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        // collects paper, adds 10 health if health is below 90
        if (other.gameObject.CompareTag("hw"))
        {
            other.gameObject.SetActive(false);
            hw--;
            hwText.text = "HW Left: " + hw.ToString();
            source.PlayOneShot(pickup, 1);
            if (health < 90)
            {
                health += 10;
            }
        }
        //win condition, all hw collected and goes past gate
        if (other.gameObject.CompareTag("win"))
        {
            //if(hw == 0)
            if (hw < 4) //changed for video recording
            {
                SceneManager.LoadScene("Win");
            }
        }

    }
    // damage taken from enemies
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            health = health - 3;
            source.PlayOneShot(dmg, 0.5f);
            healthText.text = "Health: " + health.ToString();
        }

    }
    // step is tied in to the animation to play
    public void step()
    {
        source.PlayOneShot(steps, 1);
    }
}
