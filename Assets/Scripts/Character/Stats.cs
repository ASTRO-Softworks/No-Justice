using UnityEngine;

public class Stats : MonoBehaviour {

    public int health = 100;
    public int defence = 100;

    public void Damage (int damage)
    {
        Debug.Log("HP " + health);
        defence -= damage;
        if (defence < 0)
        {   
            health += damage;
            defence = 0;
        }
        if (health > 0) return;
        Debug.Log("KILL "+gameObject.name);
        Destroy(gameObject);
    }
}
