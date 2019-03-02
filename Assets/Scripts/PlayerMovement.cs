using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;
    public Transform Scope;

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
    bool invisible = false;
    bool dirRight = false;


    // Use this for initialization
    void Start () {
        //gameObject.GetComponent<WeaponList>().ChangeWeapon(0);
        //new Quaternion()
        //transform.localRotation
        Scope.gameObject.GetComponent<Scope>().takeAim(Vector3.zero);
    }
	
	// Update is called once per frame
	void Update () {//Get controll from keyboard
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        verticalMove = Input.GetAxisRaw("Vertical");

        mouseX = Input.GetAxisRaw("Mouse X");

        //Scope.gameObject.GetComponent<Scope>().takeAim(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Scope.gameObject.GetComponent<Scope>().takeAim(Vector3.zero);
        animator.SetFloat("Speed",Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            onladder = false;
            //Debug.Log("NOT ON LADDER!!!");
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
        else if (Input.GetButtonDown("Interact0"))//Box hide
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
        else if (Input.GetButtonDown("Interact1"))//"Invis"
        {
            invisible = !invisible;
            if (invisible)
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
            } else
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            
        }
        else if (Input.GetButtonDown("Fire1"))//"Feiaaaar"
        {
            Scope.gameObject.GetComponent<Scope>().Shoot(true);
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
        dirRight = ((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.localPosition.x) > 0);//horizontalMove > 0?true:horizontalMove<0?false:dirRight;//
        //Debug.Log(dirRight);
        //Debug.Log("NearLadder " + nearladder.ToString() + "\nOnladder " + onladder.ToString());
        controller.Move(new Vector2(horizontalMove,verticalMove) * Time.fixedDeltaTime, dirRight, crouch, jump, nearladder&&onladder, swimming); 
        jump = false;
    }
}
