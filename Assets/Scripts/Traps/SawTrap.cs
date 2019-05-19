using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [SerializeField] private Vector2 speed;
    [SerializeField] private float distance;
    [SerializeField] private int damage;
    private float modspeed;
    private float _dist = 0;
    private bool dir = false;

    // Start is called before the first frame update
    void Start()
    {
        modspeed = speed.magnitude;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Stats Char = col.gameObject.GetComponent<Stats>();
        if (Char) Char.Damage(damage);
    }

    // Update is called once per frame
    void Update()
    {
        if (dir)
        {
            if (_dist >= distance){ 
                dir = false;
                return;
            }
            transform.position += (Vector3)speed * Time.deltaTime;
            _dist += modspeed * Time.deltaTime;
        }
        else
        {
            if (_dist <= 0) {
                dir = true;
                return;
            }
            transform.position -= (Vector3)speed * Time.deltaTime;
            _dist -= modspeed * Time.deltaTime;
        }
    }
}

