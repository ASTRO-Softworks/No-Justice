
using UnityEngine;
public class MeleeWeapon : Weapon
{
    public override void Shoot()
    {
        transform.parent.parent.GetComponent<Animator>().SetBool("isMeleeWeaponAttack", true);
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, distance);
        if (objects.Length > 0)
            foreach (Collider2D obj in objects)
            {
                if (obj.gameObject.CompareTag(transform.parent.parent.gameObject.GetComponent<Stats>().enemyTeam))
                {
                    obj.gameObject.GetComponent<Stats>().Damage(Damage);
                    obj.gameObject.GetComponent<Stats>().SetMemory(transform.position);
                    
                    break;
                }
            }
    }
}