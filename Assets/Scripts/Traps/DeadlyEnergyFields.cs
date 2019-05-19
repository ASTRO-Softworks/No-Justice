using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyEnergyFields : MonoBehaviour
{
    public float starttime = 5;
    public float time = 1;
    public float timeattack = 1;
    private Stats _st;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D Player)
    {
        if (time < timeattack)
        {
            if (_st = Player.gameObject.GetComponent<Stats>())
            {
                _st.Damage(50);
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time < timeattack)
        {
            this.gameObject.GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
    
    void FixedUpdate()
    {
        if(time > 0)
        {
            time -= Time.fixedDeltaTime;
        }
        else
        {
            time = starttime;
        }
    }
}