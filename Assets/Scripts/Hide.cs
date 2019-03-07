using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{

    public GameObject m_obj;
    public bool isUsed=false;
    public Rigidbody2D rb;
    public List<GameObject> list;

    //private float pTime = 0;
    //private float pPos = 0;
    //private float dal=0.05f;
    //private float vel = 0f;
    //private float walkingOffset = 0.5f;
    //private float crouchingOffset = 0.1f;
    //private float m_MovementSmoothing = 0.5f;
    private Vector3 m_Pos;

    public void Use(GameObject obj, bool use)
    {
        
        if (use) {

            isUsed = !isUsed;
        }
        m_obj = obj;
        //vel = m_obj.GetComponent<Rigidbody2D>().velocity.x;
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (isUsed)
        {
            Vector3 offset = new Vector3(0f, -0.25f, 0f);
/*            if (m_obj.GetComponent<CharacterController2D>().m_Walking)
            {
                if (m_obj.GetComponent<CharacterController2D>().m_wasCrouching)
                {
                    offset += new Vector3(0f, crouchingOffset, 0f);
                }
                else
                {
                    offset += new Vector3(0f, walkingOffset, 0f);
                }
            }
            */
            transform.localPosition = m_obj.transform.localPosition;
            //vel = (1 - dal) * vel + dal * Vector3.Magnitude(m_obj.GetComponent<Rigidbody2D>().velocity);
            //vel = (1 - dal) * vel + dal * m_obj.GetComponent<Rigidbody2D>().velocity.x;
            //m_obj.transform.localPosition + offset
            //transform.localPosition= Vector3.SmoothDamp(transform.localPosition, m_obj.transform.localPosition + offset, ref m_Pos, m_MovementSmoothing);
            transform.localPosition = transform.localPosition + offset;
            Debug.Log(m_obj.GetComponent<Rigidbody2D>().velocity.ToString());
            //transform.localPosition.y = transform.localPosition.y - 0.25 + Rigidbody2D.velocity.x;
        }
	}
}
