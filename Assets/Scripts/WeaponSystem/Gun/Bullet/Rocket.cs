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
    private List<GameObject> objs;
    private GameObject ignoreObj;
    protected override void Start()
    {
        base.Start();
        box = GetComponent<BoxCollider2D>();
        objs = new List<GameObject>();
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
        if (!(layerIndexes.Contains(collision.gameObject.layer)))
            return;

        if (hitObject != shooter && !objs.Contains(hitObject) && hitObject!=ignoreObj)
        {
            Enemy enemy = hitObject.GetComponent<Enemy>();
            objs.Add(hitObject);

            RocketExplosion obj = Instantiate(explosion, transform.position, Quaternion.identity);
            obj.SetDmg(ExplosionDmg);
            obj.SetShooter(shooter);
            obj.IfIcyAttack(iceBullet);
            SpawnMicroRockets();
            Destroy(obj, 0.7f);

            if (hitObject.GetComponent<Hurtable>())
            {
                hitObject.GetComponent<Hurtable>().Hurt(dmg);
            } else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetMicroRocketNum(int num)
    {
        microRockNum = num;
    }

    private void SpawnMicroRockets()
    {
        int num = microRockNum;
        if (microRocket)
        {
            while (num > 0)
            {
                Debug.Log("spawn " + num);
                int angle = (360/microRockNum) * num;
                Vector2 dir = -(transform.position - initPosition);
                Bullet bullet = Instantiate(microRocket, transform.position, Quaternion.Euler(0, 0, angle));
                InitBullet((Rocket)bullet);
                num--;
            }
        }
    }

    protected void InitBullet(Rocket bullet)
    {
        bullet.SetShooter(shooter);
        bullet.SetLifeTime(lifeTime / 5);
        bullet.SetSpeed(speed / 3);
        bullet.SetIgnoreObj(hitObject);
    }

    public void SetIgnoreObj(GameObject obj)
    {
        ignoreObj = obj;
    }

    protected override void DestroyProjectile()
    {
        RocketExplosion obj = Instantiate(explosion, transform.position, Quaternion.identity);
        obj.SetDmg(ExplosionDmg);
        Destroy(this.gameObject);
    }
}