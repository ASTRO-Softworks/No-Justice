using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
    public float climbSpeed = 2f;

    float horizontalMove = 0f;

    float verticalMove = 0f;

    bool jump = false;
    bool crouch = false;
    bool Nearladder = false;
    bool Onladder = false;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        verticalMove = Input.GetAxisRaw("Vertical") * climbSpeed;

        animator.SetFloat("Speed",Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            Onladder = false;
            Debug.Log("NOT ON LADDER!!!");
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            //animator.SetBool("IsCrouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            //animator.SetBool("IsCrouching", false);
        }
        else if (Input.GetButtonDown("Interact"))
        {
            if (Nearladder)
            {
                Onladder = true;
            }
            //animator.SetBool("IsCrouching", false);
        }


    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            Nearladder = true;

            Debug.Log("Ladder");
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            Nearladder = false;
            //Onladder = false;
            Debug.Log("NotLadder");
            
        }

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        Onladder = false;
        Debug.Log("NOT ON LADDER 'COS LANDED!!!");
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        Debug.Log("NearLadder " + Nearladder.ToString() + "\nOnladder " + Onladder.ToString());
        controller.Move(new Vector2(horizontalMove,verticalMove) * Time.fixedDeltaTime, crouch, jump, Nearladder&&Onladder); 
        jump = false;
    }
}
