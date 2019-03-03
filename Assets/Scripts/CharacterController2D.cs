using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [SerializeField] private float m_FlyingSpeed = 800f;                         // Flying speed multiplier
    [SerializeField] private float m_WalkingSpeed = 10f;                        // Walking speed multiplier
    [SerializeField] private float m_ClimbingSpeed = 6f;                        // Climbing speed multiplier
    [SerializeField] private float m_GravityScale = 1f;                        // Climbing speed multiplier
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;           // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_wasClimbing;
    private bool m_Climbing;
    public bool m_Walking;//PRIVATE
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private bool m_wasFacingRight = true; // Where player was facing before climbing
    private Vector2 m_Velocity = Vector2.zero;
    //private String condition = "Walk";
    public enum Condition
    {
        Walk,
        Crouch,
        //Climb,
        //Swim,
        Fly,
        LEN

    }
    //BitArray wasDoing = new BitArray((int)Condition.LEN, false);
    //BitArray wantDoing = new BitArray((int)Condition.LEN, false);
    Condition condition;


    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    public bool m_wasCrouching = false;//PRIVATE
    
    
    
    
    private void Awake()
    {
        

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.gravityScale = m_GravityScale;
        Debug.Log("Setting gravity to " + m_GravityScale.ToString());
    }

    private void FixedUpdate()
    {
        //Update Grounded
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && !collider.isTrigger)
            {
                //Debug.Log("Here i am!"+ wasGrounded.ToString());
                //condition = "Walk";
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }

        //Debug.Log("Grounded: "+m_Grounded.ToString()+"\n"+ "wasGrounded: "+wasGrounded.ToString());
    }
    /*
    public void Climb(float climb)
    {
        if (!m_wasClimbing)
        {
            m_Rigidbody2D.gravityScale = 0;
        }



        Vector3 targetVelocity = new Vector2(0, climb * 10f);
        
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


        m_wasClimbing = true;
    }
    */

    public void Move(Vector2 moveDirection, Vector2 lookDirection)
    {

        //√оризонтальна€ составл€юща€ вектора движени€
        // m_Walking = (direction.x !=0);
        // finding the target velocity
        //Vector3 targetVelocity = new Vector2(direction * 10f, m_Rigidbody2D.velocity.y);//Keeping current horisontal velocity otherwise   
        // Move the character 
        //m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


        //≈сли игрок идет не в сторону куда смотрит ...
        if ((lookDirection.x > 0) ^ m_FacingRight && (lookDirection.x != 0))
        {
            // ... flip the player.
            Flip();
        }

        //выбираем сценарий работы контроллера
        switch (condition)
        {
            case Condition.Fly: _Logic_Fly(moveDirection); break;
            case Condition.Walk: _Logic_Walk(moveDirection); break;
            case Condition.Crouch: _Logic_Crouch(moveDirection); break;
            //case Condition.Climb: _Logic_Climb(moveDirection); break;
        }


    }

    public void Jump()
    {
        if (m_Grounded)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            Debug.Log("Jumping with force:" + m_JumpForce.ToString());
        }
       
    }

    private void _Mechanics_Destruct_Manager()
    {
        switch (condition)
        {
            case Condition.Fly: return;
            case Condition.Crouch: _Destruct_Crouch(); break;
            case Condition.Walk: _Destruct_Walk(); break;
        }
        condition = Condition.Fly;
    }

    public bool Toggle_Fly()
    {
        _Mechanics_Destruct_Manager();
        return true;
    }

    
    /*public void Toggle_Swim(bool flag)
    {

        wantDoing.Set((int)Condition.Swim, flag);

        

        if (flag)//We want to Swim
        {
            if (condition == Condition.Swim)//We were swimming already
            {
                wasDoing.Set((int)Condition.Swim, true);//Notify corresponding function
            }
            else //We weren't swimming before

            if (Condition.Swim != condition)
            {
                if (condition != Condition.Climb)//Condition at which we can Swim
                {
                    condition = Condition.Swim;
                }
            }
        }
        else//We dont want to Swim
        {
            if (true)//Condition at which we can stop Swimming
            {
                //wasDoing
                condition = Condition.Walk;
                FixedUpdate();//To be sure is is called before Move
            }
        }
    }
    */
    /*public void Toggle_Climb(bool flag)
    {
        wantDoing.Set((int)Condition.Climb, flag);
        //condition = "Climb";
    }*/
    /*public void Toggle_Start(Condition condition, bool flag)
    {
        wantDoing.Set((int)condition, flag);
        //this.condition = condition;
    }*/

    private void _Logic_Fly(Vector2 direction)
    {
        //m_Rigidbody2D.AddForce(Vector3.Scale(targetVelocity,new Vector3(5f,1f,0.1f)));

        //Getting target velocity
        Vector2 targetVelocity = new Vector2(direction.x * m_FlyingSpeed, direction.y * m_FlyingSpeed);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    public void Toggle_Walk()
    {
        _Mechanics_Destruct_Manager();

        //Enabling gravity
        m_Rigidbody2D.gravityScale = m_GravityScale;

        condition = Condition.Walk;
    }

    private void _Logic_Walk(Vector2 direction)
    {
        //m_Rigidbody2D.AddForce(Vector3.Scale(targetVelocity,new Vector3(5f,1f,0.1f)));

        //Getting target velocity
        Vector2 targetVelocity = new Vector2(direction.x * m_WalkingSpeed, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
    private void _Destruct_Walk()
    {
        m_Rigidbody2D.gravityScale = 0;
    }

    public void Toggle_Crouch()
    {
        _Mechanics_Destruct_Manager();

        //Enabling gravity
        m_Rigidbody2D.gravityScale = m_GravityScale;
        // Disable one of the colliders when crouching
        if (m_CrouchDisableCollider != null)
            m_CrouchDisableCollider.enabled = false;

        condition = Condition.Crouch;

    }
    private void _Logic_Crouch(Vector2 direction)
    {
        //m_Rigidbody2D.AddForce(Vector3.Scale(targetVelocity,new Vector3(5f,1f,0.1f)));

        //Getting target velocity
        Vector2 targetVelocity = new Vector2(direction.x * m_WalkingSpeed * m_CrouchSpeed, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
    private void _Destruct_Crouch()
    {
        m_Rigidbody2D.gravityScale = 0;
        // Enable the collider when not crouching
        if (m_CrouchDisableCollider != null)
            m_CrouchDisableCollider.enabled = true;
    }

    /*private void _Logic_Climb(Vector2 direction)
    {
        Vector2 targetVelocity = new Vector2(direction.x * m_ClimbingSpeed, direction.y * m_ClimbingSpeed);
        //m_Velocity = m_Rigidbody2D.velocity;
        m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }*/

    public void DinosaurMove(Vector2 tr, bool dirRight, bool crouch, bool jump, bool climbing, bool swimming)
    {
        if (climbing || jump) crouch = false;

        float hor = tr.x;
        float ver = tr.y;
        m_Walking = (hor != 0);
        m_Climbing = climbing;

        if (!m_wasClimbing && climbing)
        {
            //m_Climbing = true;
            m_Grounded = true;
            //Remember direction before climbing
            m_wasFacingRight = m_FacingRight;

            /*
            //Turning right
            if (!m_FacingRight)
            {
                Flip();
            }
            */

            //No gravity while climbing
            m_Rigidbody2D.gravityScale = 0;
        }

        // Move the character by finding the target velocity
        //m_Rigidbody2D.velocity.x

        //Vector3 targetVelocity = new Vector2(0, climb * 10f);

        // And then smoothing it out and applying it to the character

        // m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (m_wasClimbing && !climbing)
        {
            //Turning as it was before climbing
            //m_Climbing = false;
            if (m_wasFacingRight != m_FacingRight)
            {
                Flip();
            }

            //Return gravity to normal value
            m_Rigidbody2D.gravityScale = m_GravityScale;
        }
        //----------------------------------------------------------------------------------------------------------------
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            Collider2D Col = Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround);
            if (Col && !Col.isTrigger)
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl || climbing)
        {

            // If crouching
            if (crouch && !climbing && !swimming)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                hor *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            float targety;                          //Target horisontal velocity
            if (climbing)
            {
                targety = ver * 10f;              //If climbing fixing horisontal velocity
            }
            else if (swimming)
            {
                targety = ver * 10f;
                //Debug.Log("Swimming: " + targety.ToString());
            }
            else
            {
                targety = m_Rigidbody2D.velocity.y; //Keeping current horisontal velocity otherwise
            }

            Vector3 targetVelocity = new Vector2(hor * 10f, targety);
            // And then smoothing it out and applying it to the character
            //m_Velocity = m_Rigidbody2D.velocity;
            //if (swimming) m_MovementSmoothing = 0.5f;
            //m_MovementSmoothing = 0;
            if (swimming)
            {
                //m_Rigidbody2D.AddForce(Vector3.Scale(targetVelocity,new Vector3(5f,1f,0.1f)));
                m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
            else
            {
                m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
            if (!climbing)
            {
                // If the input is moving the player right and the player is facing left...
                if (dirRight && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (!dirRight && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
        }
        // If the player should jump...
        if ((m_Grounded || climbing) && !swimming && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }

        m_wasClimbing = climbing;
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        transform.Rotate(0f, 180f, 0f);
    }
}
