﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : Enemy
{
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
