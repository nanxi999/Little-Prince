using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    public float laserPushBack;
    public float dmg = 30;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Prince prince = collision.gameObject.GetComponent<Prince>();
        if (prince)
        {
            Vector3 dir = prince.transform.position - transform.position;
            if (dir.magnitude != 0)
            {
                prince.PushBack(dir / dir.magnitude * laserPushBack, 0.5f);
            }
            prince.Hurt(dmg);
        }
    }
}
