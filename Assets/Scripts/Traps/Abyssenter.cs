using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abyssenter : MonoBehaviour
{
    private AbstractCharacter _ac;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D Player)
    {
        if (_ac = Player.gameObject.GetComponent<AbstractCharacter>())
        {
            _ac.Die();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
