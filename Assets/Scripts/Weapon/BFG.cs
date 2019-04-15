using UnityEngine;

public class BFG : Weapon
{

    private Transform firePoint;
    //public GameObject bulletPrefab;
    public LayerMask whatToHit;
    //public GameObject pew;
    //int layerMask = whatToHit;//~(1 << 2);

    void Start()
    {
        distance = 10;

        //        fireRate = 1.5f;
        //        Damage = 10.0f;
        //        bulletSpeed = 1.0f;
        firePoint = transform.Find("FirePoint");
    }

    public override void Shoot()
    {
        float rotConst = (float)((((transform.rotation.eulerAngles.y / 180 - 1) * 2) + 1) * -1);
        
        Vector2 dir = new Vector2(1, Mathf.Tan(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * rotConst) * rotConst;
        Vector2 speed = dir.normalized;
        
        RaycastHit2D hit  = Physics2D.Raycast(firePoint.position, dir, distance,whatToHit);
        if (hit)
        {
            Debug.Log("Hit " + hit.collider.gameObject.name);
            Debug.DrawLine(firePoint.position, hit.point, Color.red);
        }
        Debug.DrawRay(firePoint.position, dir,Color.blue);
    }
}