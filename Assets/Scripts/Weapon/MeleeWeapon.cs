
using UnityEngine;
public class MeleeWeapon : Weapon
{
    private Transform firePoint;
//    public GameObject bulletPrefab;
    public LayerMask whatToHit;
//    public GameObject pew;
    public float distance = 10;
    //int layerMask = whatToHit;//~(1 << 2);

    void Start()
    {
        //        fireRate = 1.5f;
        //        Damage = 10.0f;
        //        bulletSpeed = 1.0f;
        firePoint = transform.Find("FirePoint");
    }

    public override void Shoot()
    {
        Debug.Log(transform.parent.parent);
        transform.parent.parent.GetComponent<Animator>().SetBool("isMeleeWeaponAttack", true);
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, 1);
        if (objects.Length > 0)
            foreach (Collider2D obj in objects)
            {
                if (obj.gameObject.CompareTag("Enemy"))
                {
                    obj.gameObject.GetComponent<Stats>().Damage(Damage);
                    
                    break;
                }
            }
        /*
        float rotConst = (float)((((transform.rotation.eulerAngles.y / 180 - 1) * 2) + 1) * -1);
        
        Vector2 dir = new Vector2(1, Mathf.Tan(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * rotConst) * rotConst;
        Vector2 speed = dir.normalized;
        
        RaycastHit2D hit  = Physics2D.Raycast(firePoint.position, dir, distance,whatToHit);
        if (hit)
        {
            Debug.Log("Hit " + hit.collider.gameObject.name);
            Debug.DrawLine(firePoint.position, hit.point, Color.red);
        }
        Debug.DrawRay(firePoint.position, dir,Color.blue);*/
    }
}