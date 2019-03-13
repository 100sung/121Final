using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatescript : MonoBehaviour
{

    GameObject hwpart;
    GameObject hwpart1;
    GameObject hwpart2;
    GameObject hwpart3;
    GameObject hwpart4;
    //public ParticleSystem stuff;
    void Start()
    {

        
        hwpart = GameObject.Find("part");
        hwpart1 = GameObject.Find("part1");
        hwpart2 = GameObject.Find("part2");
        hwpart3 = GameObject.Find("part3");
        hwpart4 = GameObject.Find("part4");
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(20, 50, 70) * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.name == "hwpart")
            {

                hwpart.SetActive(false);
            }
            if (other.gameObject.name == "hwpart1")
            {

                hwpart1.SetActive(true);
            }
            if (other.gameObject.name == "hwpart2")
            {

                hwpart2.SetActive(true);
            }
            if (other.gameObject.name == "hwpart3")
            {

                hwpart3.SetActive(true);
            }
            if (other.gameObject.name == "hwpart4")
            {

                hwpart4.SetActive(true);
            }
        }
    }
    
}
