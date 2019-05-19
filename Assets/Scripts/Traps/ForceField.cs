using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField]private int damage;
    [SerializeField]private float timeOn = 1.0f;
    [SerializeField]private float timeOff = 2.0f;
    [SerializeField] private bool debug;
    
    
    private float _time;
    private bool _on = false;
    private SpriteRenderer _spriteRenderer;

    private Color colorOn = new Color(1, 1, 0, .5f);
    private Color colorOff = new Color(1, 0, 0, .5f);
    
    
    void OnCollisionEnter2D(Collision2D col)
    {
        
        Character.Stats Stats = col.gameObject.GetComponent<Character.Stats>();

        if (Stats)
        {
            Stats.Damage(damage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _time = _on ? timeOn : timeOff;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
        }
        else
        {
            _on = !_on;
            if (_on)
            {
                _spriteRenderer.color = colorOn;
                _time = timeOn;
            }
            else
            {
                _spriteRenderer.color = colorOff;
                _time = timeOff;
            }
        }
        
    }
}
