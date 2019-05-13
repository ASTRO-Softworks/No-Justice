using UnityEngine;

public class Press : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D Robot)
    {
        if (Robot.gameObject.CompareTag("Player") || Robot.gameObject.CompareTag("Enemy"))
            Robot.gameObject.GetComponent<Character.Stats>().Damage(100);
    }
 //   private int count = 100;
    void Update()
    {

    }
}