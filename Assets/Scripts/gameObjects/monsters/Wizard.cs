using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Wizard : Enemy
{
    [Header("Bullet Settings")]
    public DarkBullet darkBullet;
    public float bulletSpeed;
    public float bulletLifeTime;
    public Transform attackPoint;
    public float attackRange = 3f;
    public float attackCd;

    [Header("Magic Setting")]
    [SerializeField] private float IgniteCd = 3;
    [SerializeField] private float IgniteNum = 2;

    private float igniteSinceLastAtk = 0;
    private float sinceLastAttack;


    protected override void Start()
    {
        base.Start();
        GetComponent<AIPath>().maxSpeed = moveSpeed;
    }

    protected override void Update()
    {
        sinceLastAttack += Time.deltaTime;
        igniteSinceLastAtk += Time.deltaTime;

        base.Update();
        GetComponent<AIDestinationSetter>().target = prince.transform;
        FireTrigger();
    }

    public void FireTrigger()
    {
        if(Vector2.Distance(transform.position, prince.transform.position) < attackRange)
        {
            if(sinceLastAttack >= attackCd) 
            {
                sinceLastAttack = 0;
                StartCoroutine(Freeze(1));
                animator.SetTrigger("Fire");
            } 
        } else if(igniteSinceLastAtk >= IgniteCd)
        {
            Ignite();
            igniteSinceLastAtk = 0f;
        }
    }

    protected override void ChangeRendererColor(Color color)
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>(true);
        foreach (SpriteRenderer renderer in renderers)
        {
            if (!renderer.gameObject.name.Equals("ShadowRenderer"))
                renderer.color = color;
        }
    }

    public void Fire()
    {
        Vector3 dir = prince.transform.position - transform.position;
        CastDarkBullet(dir);
    }

    private void CastDarkBullet(Vector3 dir)
    {
        if (darkBullet)
        {
            float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg - 90f;
            DarkBullet db = Instantiate(darkBullet, attackPoint.transform.position, transform.rotation);
            db.SetDirection(dir);
            db.transform.Rotate(new Vector3(0, 0, angle));
            db.SetLifeTime(bulletLifeTime);
            db.SetSpeed(bulletSpeed);
            db.SetDmg(dmg);
            db.SetShooter(gameObject);
        }
    }

    private void Ignite()
    {
        animator.SetTrigger("Cast");
        int ignited = 0;
        Shadow[] shadows = FindObjectsOfType<Shadow>();
        foreach(Shadow shadow in shadows)
        {
            if(!shadow.IfIgnited()) {
                shadow.TurnExplosive();
                ignited++;
            }

            if(ignited >= IgniteNum)
            {
                break;
            }
        }
    }
}
