using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    //public GameObject Player;
    //Collider2D colider;
    Animator anim;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            anim = (Animator)collider.gameObject.GetComponent("Animator");
            anim.Play("Robot_walk");
            
            Debug.Log("Triggered!");
        }
    }
    /*
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    */
}
