using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchWizard : Enemy
{
    public Transform attackPoint;
    public float attackRange = 3f;
    public DarkBullet darkBullet;
    public float attackCd;
    private float sinceLastAttack;

    protected override void Start()
    {
        prince = FindObjectOfType<Prince>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    protected override void Update()
    {
        sinceLastAttack += Time.deltaTime;
        base.Update();
        FireTrigger();
    }

    public void FireTrigger()
    {
        if (sinceLastAttack < attackCd) { return; }
        else
        {
            sinceLastAttack = 0;
            StartCoroutine(Freeze(1));
            animator.SetTrigger("Cast");
        }
    }



    public void Fire()
    {
        if (darkBullet)
        {
            Vector3 dir = prince.transform.position - transform.position;
            float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg - 90f;
            DarkBullet db = Instantiate(darkBullet, attackPoint.transform.position, transform.rotation);
            db.SetDirection(dir);
            db.transform.Rotate(new Vector3(0, 0, angle));
            db.SetDmg(dmg);
            db.SetShooter(gameObject);
        }
    }

    public override void PushBack(Vector3 pushBackVelocity, float duration)
    {
        
    }
}
