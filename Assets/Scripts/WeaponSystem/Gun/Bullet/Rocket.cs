using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    [SerializeField] private int ExplosionDmg;
    [SerializeField] Bullet microRocket;
    [SerializeField] private RocketExplosion explosion;
    [SerializeField] private bool iceBullet;

    private GameObject hitObject;
    private int microRockNum = 0;
    private int microRockDmg = 10;
    private BoxCollider2D box;
    protected override void Start()
    {
        base.Start();
        box = GetComponent<BoxCollider2D>();
    }

    protected override void Update()
    {
        base.Update();
        Accelerate();

    }

    private void Accelerate()
    {
        speed += 85 * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        int res = LayerMask.GetMask(layers);
        hitObject = collision.gameObject;
        if (!(layerIndexes.Contains(collision.gameObject.layer)) || isColliding)
            return;

        if (hitObject != shooter)
        {
            isColliding = true;
            Enemy enemy = hitObject.GetComponent<Enemy>();
            if (hitObject.GetComponent<Hurtable>())
            {
                hitObject.GetComponent<Hurtable>().Hurt(dmg);
            }

            RocketExplosion obj = Instantiate(explosion, transform.position, Quaternion.identity);
            obj.SetDmg(ExplosionDmg);
            obj.SetShooter(shooter);
            obj.IfIcyAttack(iceBullet);
            SpawnMicroRockets();
            Destroy(obj, 2);
            Destroy(this.gameObject);
        }
    }

    public void SetMicroRocketNum(int num)
    {
        microRockNum = num;
    }

    private void SpawnMicroRockets()
    {
        Debug.Log("Spawning");
        if (microRocket)
        {
            while (microRockNum > 0)
            {
                Debug.Log("spawn " + microRockNum);
                int angle = 30 * microRockNum;
                Vector2 dir = -(transform.position - initPosition);
                Bullet bullet = Instantiate(microRocket, transform.position, Quaternion.Euler(0, 0, angle));
                InitBullet(bullet);
                microRockNum--;
            }
        }
    }

    protected void InitBullet(Bullet bullet)
    {
        bullet.SetShooter(hitObject);
        bullet.SetLifeTime(lifeTime / 4);
        bullet.SetSpeed(speed / 3);
    }

    protected override void DestroyProjectile()
    {
        RocketExplosion obj = Instantiate(explosion, transform.position, Quaternion.identity);
        obj.SetDmg(ExplosionDmg);
        Destroy(this.gameObject);
    }
}