using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Shadow : Enemy
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    private AIDestinationSetter dest;
    private float pushBackReductFactor = 0.4f;
    LayerMask layers;
    CircleCollider2D circle;

    [SerializeField] private RocketExplosion explosionVFX;
    [SerializeField] private bool explosive;
    private GameObject dieEffect;

    protected override void Update()
    {
        base.Update();
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        dest.target = prince.transform;
    }

    protected override void Start()
    {
        base.Start();
        dieEffect = deathEffect;
        animator = GetComponent<Animator>();
        circle = GetComponent<CircleCollider2D>();
        attackPoint = transform.Find("AttackPoint");    // find children "attackPoint"        
        layers = LayerMask.GetMask("Friendly");   // set mask
        dest = GetComponent<AIDestinationSetter>();
        GetComponent<AIPath>().maxSpeed = moveSpeed;
    }

    public void PlayerInRange()
    {
        StartCoroutine(Freeze(1));
        animator.SetBool("Attack", true);
    }

    public void PlayerOutOfRange()
    {
        animator.SetBool("Attack", false);
    }

    private void CheckDamage()
    {
        if (attackPoint && !explosive)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Prince prince = enemy.gameObject.GetComponent<Prince>();
                if (prince)
                {
                    prince.Hurt(dmg);
                }
            }
        }
        else if(explosive)
        {
            Hurt(health);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    protected override IEnumerator PushBackFreeze(float duration)
    {
        freeze = true;
        GetComponent<AIPath>().enabled = false;
        yield return new WaitForSeconds(duration * pushBackReductFactor);
        GetComponent<AIPath>().enabled = true;
        animator.SetBool("Hurt", false);
        freeze = false;
    }

    public override void OnDie()
    {
        if(explosive)
        {
            RocketExplosion exp = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            exp.SetDmg((int)dmg);
            Destroy(exp, 1f);
        }
        base.OnDie();
    }

    public void TurnExplosive()
    {
        explosive = true;
        moveSpeed += 10;
        dmg += 15;
        health = GetMaxHealth() * 2;
        ChangeRendererColor(Color.red);
    }

    public bool IfIgnited()
    {
        return explosive;
    }
}
