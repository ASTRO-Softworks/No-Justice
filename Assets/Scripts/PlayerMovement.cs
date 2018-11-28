using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
    public float climbSpeed = 20f;
    public float diveSpeed = 100;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    float mouseX = 0f;
    float mousey = 0f;

    bool jump = false;
    bool crouch = false;
    bool nearladder = false;
    bool onladder = false;
    bool swimming = false;
    

    // Use this for initialization
    void Start () {
        //new Quaternion()
        //transform.localRotation
	}
	
	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        verticalMove = Input.GetAxisRaw("Vertical");

        mouseX = Input.GetAxisRaw("Mouse X");


        animator.SetFloat("Speed",Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            onladder = false;
            //Debug.Log("NOT ON LADDER!!!");
            animator.SetBool("IsJumping", true);
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
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
        }
        else if (verticalMove>0)
        {
            if (nearladder)
            {
                onladder = true;
            }
            //animator.SetBool("IsCrouching", false);
        }

        if (onladder) verticalMove *= climbSpeed;
        else if (swimming) {
            //Debug.Log("Diving Down! " + verticalMove.ToString());
            verticalMove = verticalMove *diveSpeed;
            //Debug.Log("Diving Down! "+verticalMove.ToString()+" "+diveSpeed.ToString());
        }
        //Debug.Log("Diving Down! " + verticalMove.ToString() + " " + diveSpeed.ToString());

    }

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

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        onladder = false;
        //Debug.Log("NOT ON LADDER 'COS LANDED!!!");
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        //Debug.Log("NearLadder " + nearladder.ToString() + "\nOnladder " + onladder.ToString());
        controller.Move(new Vector2(horizontalMove,verticalMove) * Time.fixedDeltaTime, crouch, jump, nearladder&&onladder, swimming); 
        jump = false;
    }
}
