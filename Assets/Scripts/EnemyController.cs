using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : AbstractCharacter {
    /* Видимо не нужно уже
    public CharacterController2D controller;
    public Animator animator;
    public Transform Scope;
    private Vector3 resp;

    public float runSpeed = 40f;
    public float climbSpeed = 20f;
    public float diveSpeed = 100;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    float mouseX = 0f;
    float mousey = 0f;
    */
   //Пусть пока побудет
    bool jump = false;
    bool crouch = false;
    bool nearladder = false;
    bool onladder = false;
    bool swimming = false;
    bool invisible = false;
    bool dirRight = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            nearladder = true;

            //Debug.Log("Ladder");
        }
        else if (collider.CompareTag("Water"))
        {
            swimming = true;
        }

    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            nearladder = false;
            //Onladder = false;
            //Debug.Log("NotLadder");

        }
        else if (collider.CompareTag("Water"))
        {
            swimming = false;
        }

    }
    
    // Use this for initialization
    void Start () {

       // Scope = transform.Find("Aimer");
     //   resp = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Collider2D[] cld = Physics2D.OverlapCircleAll(transform.position, 1);
        Walk();        
        for(int i = 0; i < cld.Length; i++)
        {
            if (cld[i].gameObject.CompareTag("Player"))
            {
                Crouch();
                //Scope.gameObject.GetComponent<Scope>().takeAim(cld[i].transform.position);
            }
             
        }
	}

    void FixedUpdate()
    {   runSpeed = (float)Math.Sin(Time.time)/3;
        controller.Move(new Vector2(runSpeed, 0), new Vector2(runSpeed, 0));
    }
}