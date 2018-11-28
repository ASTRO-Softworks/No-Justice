using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_wasClimbing;
    private bool m_Climbing;
    public bool m_Walking;//PRIVATE
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private bool m_wasFacingRight = true; // Where player was facing before climbing
    private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
    public bool m_wasCrouching = false;//PRIVATE
    private float m_GravityScale;

    private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_GravityScale=m_Rigidbody2D.gravityScale;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger)
			{
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
	public void Move(Vector2 tr, bool crouch, bool jump, bool climbing, bool swimming)
	{
        if (climbing || jump) crouch = false;

        float hor = tr.x;
        float ver = tr.y;
        m_Walking = (hor !=0);

        if (!m_wasClimbing && climbing)
        {
            //m_Grounded = false;

            //Remember direction before climbing
            m_wasFacingRight = m_FacingRight;
            
            //Turning right
            if (!m_FacingRight)
            {
                Flip();
            }

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
            if (m_wasFacingRight != m_FacingRight)
            {
                Flip();
            }

            //Return gravity to normal value
            m_Rigidbody2D.gravityScale = 3;
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
            } else
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
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
            else {
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
            if (!climbing)
            {
                // If the input is moving the player right and the player is facing left...
                if (hor > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (hor < 0 && m_FacingRight)
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
        /*
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
        */
	}
}
