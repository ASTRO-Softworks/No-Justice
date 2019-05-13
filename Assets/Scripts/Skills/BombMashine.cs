/*
 * Developer:    Sergeev Sergey
 * Contact:      https://vk.com/sergeev_1999
 * Version:      1.0.0
 * Date:         13.05.19
 * class for creating Light explosion objects
 */

using UnityEngine;

namespace Skills
{
    public class BombMashine : Skill
    {
        public GameObject Bomb;
        
        public override void Active()
        {
            Instantiate(Bomb, transform.parent.transform);
        }
    }
}