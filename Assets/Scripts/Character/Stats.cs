using UnityEngine;

public class Stats : MonoBehaviour {

    public int health = 100;
    public int defence = 100;

    public void Damage (int damage)
    {
        defence -= damage;
        
        if (defence < 0)
        {   
            health += defence;
            defence = 0;
        }
        Debug.Log("DEF " + defence);
        Debug.Log("HP " + health);

        if (health > 0) return;
        Debug.Log("KILL "+gameObject.name);
        Destroy(gameObject);
    }
}
