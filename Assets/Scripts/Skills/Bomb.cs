/*
 * Developer:    Sergeev Sergey
 * Contact:      https://vk.com/sergeev_1999
 * Version:      1.0.0
 * Date:         13.05.19
 * class for Light explosion objects
 */
using UnityEngine;
using UnityEngine.Networking;

namespace Skills
{
    public class Bomb : MonoBehaviour
    {
        public int activeTimeDuration = 500;
        public Animator animator;
        public float radius;
        public float waitTime;
        
        private void Start()
        {
            animator.SetBool("Boom", true);

            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (var collider2D in collider2Ds)
            {
                if (collider2D.CompareTag("Enemy"))
                {
                    collider2D.gameObject.transform.Find("Aimer").gameObject.GetComponent<Scope>().SetTimeToFire(waitTime);
                }
            }
        }

        public void FixedUpdate()
        {
            activeTimeDuration--;
            if (activeTimeDuration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}