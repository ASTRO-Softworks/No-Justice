using UnityEngine;

namespace Skills
{
    public class Shield : MonoBehaviour
    {
        public int activeTimeDuration = 500;

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