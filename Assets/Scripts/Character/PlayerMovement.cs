﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : AbstractCharacter
{

    //public CharacterController2D controller;
    //public Animator animator;
//    public Transform Scope;

    
    public float climbSpeed = 20f;
    public float diveSpeed = 100;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    //float mouseX = 0f;
    //float mousey = 0f;
    /*
    bool jump = false;
    bool crouch = false;
    bool nearladder = false;
    bool onladder = false;
    bool swimming = false;
    */
    bool invisible = false;
    //bool dirRight = false;


    // Use this for initialization
    void Start()
    {
        runSpeed = 40f;
        //gameObject.GetComponent<WeaponList>().ChangeWeapon(0);
        //new Quaternion()
        //transform.localRotation
        transform.Find("Aimer").gameObject.GetComponent<Scope>().takeAim(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {//Get controll from keyboard
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        verticalMove = Input.GetAxisRaw("Vertical");

        //mouseX = Input.GetAxisRaw("Mouse X");

        //Scope.gameObject.GetComponent<Scope>().takeAim(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        transform.Find("Aimer").gameObject.GetComponent<Scope>().takeAim(Vector3.zero);
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            //      jump = true;
            //    onladder = false;
            //Debug.Log("NOT ON LADDER!!!");
            Jump();
        }

        if (Input.GetButtonDown("Crouch"))
        {
            //  crouch = true;
            Crouch();
            animator.SetBool("IsCrouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            //crouch = false;
            Crouch();
            animator.SetBool("IsCrouching", false);
        }
        else if (Input.GetButtonDown("Interact1"))//Box hide //Fly
        {
            Fly();
            /*
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 1f);
            foreach(Collider2D i in col)
            {
                if (i.CompareTag("Hide"))
                {
                    i.gameObject.GetComponent<Hide>().Use(gameObject, true); ;
                    //i.Use(gameObject.transform, true);
                    //Debug.Log(i.name);
                }
            }
            */
        }
        else if (Input.GetButtonDown("Interact2"))//"Invis"
        {
            invisible = !invisible;
            if (invisible)
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }

        }
        else if (Input.GetButtonDown("Fire1"))//"Feiaaaar"
        {
            transform.Find("Aimer").gameObject.GetComponent<Scope>().Shoot();
        }
        else if (Input.GetButtonDown("Interact0"))//Ladder
        {
            //Debug.Log("PM_Climb");
            Climb();
            //animator.SetBool("IsCrouching", false);
        }

        if (controller.Condition == CharacterController2D.State.Climb) verticalMove *= climbSpeed;
        else if (controller.Condition == CharacterController2D.State.Swim)
        {
            //Debug.Log("Diving Down! " + verticalMove.ToString());
            //verticalMove = verticalMove *diveSpeed;
            //Debug.Log("Diving Down! "+verticalMove.ToString()+" "+diveSpeed.ToString());
        }
        //Debug.Log("Diving Down! " + verticalMove.ToString() + " " + diveSpeed.ToString());
    }



    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        // onladder = false;
        //Debug.Log("NOT ON LADDER 'COS LANDED!!!");
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    override protected void _FixedUpdate()
    {
        //dirRight = horizontalMove > 0?true:horizontalMove<0?false:dirRight;//((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.localPosition.x) > 0);
        //Debug.Log(dirRight);
        //Debug.Log("NearLadder " + nearladder.ToString() + "\nOnladder " + onladder.ToString());
        //controller.Move(new Vector2(horizontalMove,verticalMove) * Time.fixedDeltaTime, dirRight, crouch, jump, nearladder&&onladder, swimming);
        controller.Move(new Vector2(horizontalMove, verticalMove), Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position);
        //jump = false;
    }
}