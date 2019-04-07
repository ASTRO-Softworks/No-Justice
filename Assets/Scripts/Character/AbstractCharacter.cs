using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using S = CharacterController2D.State;

public abstract class AbstractCharacter : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    [SerializeField] private bool b_Crouch_strobe = false;
    [SerializeField] private bool b_Fly_strobe = false;

    //Enviroment
    int i_water_counter = 0;
    int i_ladder_counter = 0;
    //bool b_near_ladder = false;

    //Requests(keys)	
    bool b_req_fly = false;
    bool b_crouch = false;
    bool b_interact = false;

    //STATE AUTOMATA CRUNCHES

    public delegate bool Condition(AbstractCharacter _);


    protected class C //Conditions handler
    {
        public static bool WantToFly(AbstractCharacter _) { return _.b_req_fly; }
        public static bool WantToCrouch(AbstractCharacter _) { return _.b_crouch; }
        public static bool Interacting(AbstractCharacter _) { return _.b_interact; }
        public static bool AllInWater(AbstractCharacter _) { return _.i_water_counter >= 2; }
        public static bool HalfInWater(AbstractCharacter _) { return _.i_water_counter == 1; }
        public static bool InWater(AbstractCharacter _) { return _.i_water_counter > 0; }
        public static bool NearLadder(AbstractCharacter _)
        {
            return _.i_ladder_counter > 0;
        }
    }

    public float runSpeed;



    //()=>{};

    Condition[,] TransTable = new Condition[(int)S.LEN, (int)S.LEN]

        {           //Walk                                                  //Crouch                                                    //Climb                                                 //Swim                                                                      //Fly
        /*Walk*/    {(_)=>{return false; },                                 (_)=>{return C.WantToCrouch(_); },                          (_)=>{return C.Interacting(_) && C.NearLadder(_); },    (_)=>{return C.AllInWater(_); },                                            (_)=>{return C.WantToFly(_); }},    //Walk
        /*Crouch*/  {(_)=>{return !C.WantToCrouch(_); },                    (_)=>{return false; },                                      (_)=>{return C.Interacting(_) && C.NearLadder(_); },    (_)=>{return C.InWater(_); },                                               (_)=>{return C.WantToFly(_); }},    //Crouch
        /*Climb*/   {(_)=>{return C.Interacting(_) || !C.NearLadder(_); },  (_)=>{return false; },                                      (_)=>{return false; },                                  (_)=>{return C.AllInWater(_) && (C.Interacting(_) || !C.NearLadder(_)); },  (_)=>{return C.WantToFly(_); }},    //Climb
        /*Swim*/    {(_)=>{return !C.AllInWater(_); },                      (_)=>{return !C.InWater(_) && C.WantToCrouch(_); },         (_)=>{return C.Interacting(_) && C.NearLadder(_); },    (_)=>{return false; },                                                      (_)=>{return C.WantToFly(_); }},    //Swim
        /*Fly*/     {(_)=>{return !C.WantToFly(_); },                       (_)=>{return !C.WantToFly(_) && C.WantToCrouch(_); },       (_)=>{return C.Interacting(_) && C.NearLadder(_); },    (_)=>{return !C.WantToFly(_) && C.AllInWater(_); },                         (_)=>{return false; }}              //Fly
        };


    void Start()
    {
        //  Scope.gameObject.GetComponent<Scope>().takeAim(Vector3.zero);
    }


    protected abstract void _FixedUpdate();
    void FixedUpdate()
    {
        _FixedUpdate();
        for (int i = 0; i < (int)S.LEN; i++)
        {
            if (TransTable[(int)controller.Condition, i](this))
            {

                controller.Toggle((S)i);
                break;
                //animator.//Do something
            }
        }
        b_interact = false;
        if (b_Crouch_strobe) b_crouch = false;//b_req_fly
        if (b_Fly_strobe) b_req_fly = false;
    }

    public abstract void _Die();

    public void Die()
    {
        _Die();
    }
<<<<<<< HEAD

=======
    */
	
	public abstract void _Die();
    public void Die()
    {
        _Die();
    }
	
	
>>>>>>> 99f83ab94ab751d498903c6f14641d9c0e303d15
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Water"))
        {
            i_water_counter += 1;
        }
        else if (collider.CompareTag("Ladder"))
        {
            i_ladder_counter += 1;
        }
        //Debug.Log("Water in:" + water_counter.ToString());
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Water"))
        {
            i_water_counter -= 1;
        }
        else if (collider.CompareTag("Ladder"))
        {
            i_ladder_counter -= 1;
        }
        //Debug.Log("Water out:" + water_counter.ToString());
    }

    protected bool Climb()
    {
        if (C.NearLadder(this))
        {
            //Debug.Log("AC_Climb");
            b_interact = true;
        }
        //controller.Toggle_Climb();
        //animator.SetBool("IsClimbing", true);

        return true;
    }
    protected bool Fly()
    {
        if (b_Fly_strobe) b_req_fly = true;
        else b_req_fly = !b_req_fly;
        //b_req_fly = true;
        //controller.Toggle_Fly();
        //animator.SetBool("IsFlying", true);

        return true;
    }
    protected bool Crouch()
    {
        //

        if (b_Crouch_strobe) b_crouch = true;
        else b_crouch = !b_crouch;
       // Debug.Log("AC_Crouch " + b_crouch.ToString());
        //controller.Toggle_Crouch();
        //animator.SetBool("IsCrouching", true);

        return true;
    }
    protected bool Walk()
    {
        b_crouch = false;
        b_req_fly = false;
        //controller.Toggle_Walk();
        //animator.SetBool("IsCrouching", false);
        animator.SetFloat("Speed", runSpeed);

        return true;
    }
    protected bool Jump()
    {
        controller.Jump();
        animator.SetBool("IsJumping", true);

        return true;
    }

    protected bool Interact()
    {
        b_interact = true;
        return true;
    }
}