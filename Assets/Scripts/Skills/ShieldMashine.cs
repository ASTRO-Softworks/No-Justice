using UnityEngine;

namespace Skills
{
    public class ShieldMashine : Skill
    {
        public GameObject Shield;
        
        public override void Active()
        {
            Instantiate(Shield, transform.parent.transform);
        }
        
    }
}