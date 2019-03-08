/*
 * @author: Sergeev Sergey
 * @date: 4.03.19 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public abstract class AbstractCharacter : MonoBehaviour
{
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up


    public CharacterController2D controller;
    public Animator animator;
    //public Transform Scope;
    /* Возможно понадобится
    public float climbSpeed = 20f;
    public float diveSpeed = 100;*/
    protected float runSpeed;

    /*Надеюсь не понадобится*/
     
    float horizontalMove = 0f;
    float verticalMove = 0f;
    float mouseX = 0f;
    float mousey = 0f;

    bool jump = false;
    bool crouch = false;
    bool nearladder = false;
    bool onladder = false;
    bool inWater = false;
    //bool swimming = false;
    bool invisible = false;
    bool dirRight = false;

    void Start()
    {
      //  Scope.gameObject.GetComponent<Scope>().takeAim(Vector3.zero);
    }
    void Update () {
        
	}
    void FixedUpdate()
    {
        //оно ходит
        /* if (Math.Abs(Math.Sin(Time.time)) < 0.1)
        {
            controller.Jump();
        }*/
        
        //Массив всех объектов в радиусе видимости
        //Collider2D[] cld = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        
        /*Не знаю что пока с этим делать
        foreach(Collider2D item in cld)
        {
            if(item.tag == "Ladder")
            Debug.Log(item.tag );
        

            if (item.CompareTag("Ladder"))
                controller.Climb();
        } */      
    }
    abstract public void _OnTriggerEnter2D(Collider2D collider);
    abstract public void _OnTriggerExit2D(Collider2D collider);

    void OnTriggerEnter2D(Collider2D collider)
    {
        _OnTriggerEnter2D(collider);
        Debug.Log("AC");
        if (collider.CompareTag("Ladder"))
        {
            nearladder = true;
            //Debug.Log("Ladder");
        }
        else if (collider.CompareTag("Water"))
        {
            //swimming = true;
            inWater = true;
            Swim();
            //controller.Toggle_Swim();
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        _OnTriggerExit2D(collider);
        if (collider.CompareTag("Ladder"))
        {
            nearladder = false;
            if (onladder) controller.Toggle_Walk();
            Walk();

        }
        else if (collider.CompareTag("Water"))
        {
            //Walk();
            //controller.Toggle_Walk();
            inWater = true;
        }
    }

    void KillAnim()
    {
        switch (controller.Condition)
        {
            case CharacterController2D.e_Condition.Climb: animator.SetBool("IsClimbing", false); break;
            case CharacterController2D.e_Condition.Crouch: animator.SetBool("IsCrouching", false); break;
            case CharacterController2D.e_Condition.Fly: animator.SetBool("IsFlying", false); break;
            case CharacterController2D.e_Condition.Swim: animator.SetBool("IsSwimming", false); break;
        }
    }

    protected bool Climb()
    {
        if (controller.Condition != CharacterController2D.e_Condition.Crouch)
        {
            //Checking for ladder nearby
            Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.5f);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Ladder"))
                {
                    KillAnim();
                    controller.Toggle_Climb();
                    animator.SetBool("IsClimbing", true);
                    return true;
                }
            }
        }
        //if none found
        return false;
    }
    protected bool Swim()
    {
        if (controller.Condition != CharacterController2D.e_Condition.Climb && controller.Condition != CharacterController2D.e_Condition.Fly)
        { 
            KillAnim();
            controller.Toggle_Swim();
            animator.SetBool("IsSwimming", true);

            return true;
        }
        return false;
    }
    protected bool Fly()
    {
        if (controller.Condition != CharacterController2D.e_Condition.Crouch)
        {
            KillAnim();
            controller.Toggle_Fly();
            animator.SetBool("IsFlying", true);

            return true;
        }
        return false;
    }
    protected bool Crouch()
    {
        if (controller.Condition != CharacterController2D.e_Condition.Climb && controller.Condition != CharacterController2D.e_Condition.Fly && controller.Condition != CharacterController2D.e_Condition.Swim)
        {
            KillAnim();
            controller.Toggle_Crouch();
            animator.SetBool("IsCrouching", true);


            return true;
        }
        return false;
    }
    protected bool Walk()
    {

        if (controller.Condition == CharacterController2D.e_Condition.Crouch)
        {
            Collider2D Col = Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround);
            if (!Col || Col.isTrigger)
            {
                KillAnim();
                controller.Toggle_Walk();
                animator.SetFloat("Speed", Math.Abs(runSpeed));
                return true;
            }
            return false;
        }
        return true;
    }
    protected bool Jump()
    {
        if (controller.Condition != CharacterController2D.e_Condition.Climb) Walk();
        else if (controller.Condition != CharacterController2D.e_Condition.Climb && controller.Condition != CharacterController2D.e_Condition.Fly && controller.Condition != CharacterController2D.e_Condition.Swim)
        {
            controller.Jump();
            animator.SetBool("IsJumping", true);

            return true;
        }
        return false;
    }
}