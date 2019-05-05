using System;
using System.Collections.Generic;
using UnityEngine;

public class HackMashine : Skill
{
 //   public GameObject bulletPrefab;
    public LayerMask whatToHit;
 //   public GameObject pew;
    public float distance;
    private GameObject target = null;
    //int layerMask = whatToHit;//~(1 << 2);
    private int count = 0;
    public override void Active()
    {
        target = null;
        for (int i = 0; i < distance; i++)
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, i);
            if (objects.Length > 0)
                foreach (Collider2D obj in objects)
                {
                    Debug.Log(obj.tag + "  " + obj.gameObject);
                    if (obj.gameObject.CompareTag("Enemy"))
                        target = (obj.gameObject);
                }
        }

  /*      if (targets != null)
        {
            target = targets[count++ % targets.Count];
            count = count % 100;
        }
*/
        if (target == null)
        {
            Debug.Log("Nobody found");
        }
        else
        {
            
            target.GetComponent<EnemyController>().Hack();
            Debug.Log("Target is: " + target + "  " + target.transform.position);
        }
   /*     float rotConst = (float)((((transform.rotation.eulerAngles.y / 180 - 1) * 2) + 1) * -1);
        
        Vector2 dir = new Vector2(1, Mathf.Tan(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * rotConst) * rotConst;
        Vector2 speed = dir.normalized;
        
        RaycastHit2D hit  = Physics2D.Raycast(firePoint.position, dir, distance,whatToHit);
        if (hit)
        {
        }*/
    }
}
