using UnityEngine;

namespace Character
{
    public class Stats : MonoBehaviour
    {

        public int health = 100;
        public int defence = 100;
        public string ourTeam = "Player";
        public string enemyTeam = "Enemy";

        public void Damage(int damage)
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

            AbstractCharacter _ac = gameObject.GetComponent<AbstractCharacter>();
            if (_ac)//If charecter - kill properly
            {
                _ac.Die();
                Debug.Log("KILL " + gameObject.name);
            }
            else//Else - just destroy
            {
                Debug.Log("DESTROY " + gameObject.name);
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
            

        public void SetMemory(Vector2 vector2)
        {
            if (transform.gameObject.CompareTag("Enemy"))
                transform.gameObject.GetComponent<AI.EnemyController>().SetToMemory(vector2);
        }
    }
}