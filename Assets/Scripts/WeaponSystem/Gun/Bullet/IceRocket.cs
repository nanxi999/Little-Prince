using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRocket : Bullet
{
    [SerializeField] private int ExplosionDmg;
    private CircleCollider2D circle;
    private BoxCollider2D box;
    protected override void Start()
    {
        base.Start();
        circle = GetComponent<CircleCollider2D>();
        box = GetComponent<BoxCollider2D>();
    }

    protected override void Update()
    {
        base.Update();
        CheckCollision();
        Accelerate();

    }

    private void Accelerate()
    {
        speed += 80 * Time.deltaTime;
    }

    private void CheckCollision()
    {
        if (box.IsTouchingLayers(LayerMask.GetMask("Enemy"))) {
            Vector3 dir = transform.position - initPosition;
            Explode(dir);
            DestroyProjectile();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void Explode(Vector3 dir)
    {
        List<Collider2D> colliderList = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Enemy"));
        circle.OverlapCollider(filter, colliderList);
        foreach (Collider2D collider in colliderList)
        {
            Hurtable enemy = collider.gameObject.GetComponent<Hurtable>();
            if (enemy)
            {
                enemy.Hurt(ExplosionDmg);
                AddScoreForShooter(Mathf.RoundToInt(ExplosionDmg));
                Enemy enemyComp = enemy.GetComponent<Enemy>();

                if (enemy)
                {
                    enemyComp.IceAttackHit(4f, 0.7f);
                    enemyComp.PushBack(dir / dir.magnitude * pushBackForce, 0.7f);
                }

            }
        }
    }
}
