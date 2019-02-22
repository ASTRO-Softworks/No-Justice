using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

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

    // Use this for initialization
    void Start () {

        Scope = transform.Find("Aimer");
        resp = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Collider2D[] cld = Physics2D.OverlapCircleAll(transform.position, 100);
        for(int i = 0; i < cld.Length; i++)
        {
            if (cld[i].gameObject.CompareTag("Player"))
            {
                Scope.gameObject.GetComponent<Scope>().takeAim(cld[i].transform.position);
            }
        }
	}

    void FixedUpdate()
    {
        dirRight = (Scope.position.x - transform.localPosition.x > 0);
        //Debug.Log(dirRight);
        //Debug.Log("NearLadder " + nearladder.ToString() + "\nOnladder " + onladder.ToString());
        controller.Move(Vector2.zero, dirRight, crouch, jump, nearladder && onladder, swimming);
        jump = false;
    }
}
