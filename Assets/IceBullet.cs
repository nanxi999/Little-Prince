using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(dmg);
        int res = LayerMask.GetMask(layers);
        GameObject hitObject = collision.gameObject;
        if (!(layerIndexes.Contains(collision.gameObject.layer)) || isColliding)
            return;
        isColliding = true;

        Enemy enemy = hitObject.GetComponent<Enemy>();
        if (hitObject != shooter)
        {
            Vector3 dir = transform.position - initPosition;
            if (enemy)
            {
                if (dir.magnitude != 0)
                {
                    enemy.PushBack(dir / dir.magnitude * pushBackForce, 0.7f);
                }
                enemy.IceAttackHit(5f, 0.4f);
            }
            if (hitObject.GetComponent<Hurtable>())
            {
                hitObject.GetComponent<Hurtable>().Hurt(dmg);
            }
            DestroyProjectile();
        }
    }
}
