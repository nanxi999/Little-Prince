using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : Enemy
{
    public int dmg = 1;
    public Transform attackPoint;
    public float attackRange = 0.5f;

    LayerMask layers;
    CircleCollider2D circle;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        circle = GetComponent<CircleCollider2D>();
        attackPoint = transform.Find("AttackPoint");    // find children "attackPoint"        
        layers = LayerMask.GetMask("HittableObject");   // set mask
    }

    public int GetDmg()
    {
        return dmg;
    }

    public void SetDmg(int new_dmg)
    {
        dmg = new_dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Prince prince = collision.gameObject.GetComponent<Prince>();
        if (prince)
        {
            StartCoroutine(Freeze(1));
            animator.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("Attack", false);
    }

    private void CheckDamage()
    {
        if (attackPoint)
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
        else
        {
            Debug.Log("The attack point for mutant is not set");
        }
    }

    IEnumerator Freeze(float duration)
    {
        freeze = true;
        yield return new WaitForSeconds(duration);
        freeze = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
