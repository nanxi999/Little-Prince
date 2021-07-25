using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    [SerializeField] private int ExplosionDmg;
    private CircleCollider2D circle;
    protected override void Start()
    {
        base.Start();
        circle = GetComponent<CircleCollider2D>();
    }

    protected override void Update()
    {
        base.Update();
        
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        int res = LayerMask.GetMask(layers);
        GameObject hitObject = collision.gameObject;
        if (!(layerIndexes.Contains(collision.gameObject.layer)) || isColliding)
            return;
        isColliding = true;

        Enemy enemy = hitObject.GetComponent<Enemy>();
        if (hitObject != shooter)
        {
            Vector3 dir = transform.position - initPosition;
            if (enemy != null && dir != null)
            {
                if (dir.magnitude == 0) { return; }
                else
                {
                    enemy.PushBack(dir / dir.magnitude * pushBackForce, 0.7f);
                }
            }
            if (hitObject.GetComponent<Hurtable>())
            {
                hitObject.GetComponent<Hurtable>().Hurt(dmg);
            }
            Explode();
            DestroyProjectile();
        }
    }

    private void Explode()
    {
        List<Collider2D> colliderList = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Enemy"));
        circle.OverlapCollider(filter, colliderList);
        foreach(Collider2D collider in colliderList)
        {
           Hurtable enemy = collider.gameObject.GetComponent<Hurtable>();
            if(enemy)
            {
                enemy.Hurt(ExplosionDmg);
            }
        }
        Debug.Log("number of colliders: " + colliderList.Count);
    }
}
