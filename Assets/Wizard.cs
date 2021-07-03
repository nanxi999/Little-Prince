using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    public Transform attackPoint;
    public float attackRange = 3f;
    public DarkBullet darkBullet;

    protected override void Update()
    {
        base.Update();
        FireTrigger();
    }

    public void FireTrigger()
    {
        if(Vector2.Distance(transform.position, prince.transform.position) < attackRange)
        {
            StartCoroutine(Freeze(1));
            animator.SetTrigger("Fire");
        }   
    }

    public void Fire()
    {
        if (darkBullet)
        {
            Vector3 dir = prince.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            DarkBullet db = Instantiate(darkBullet, attackPoint.transform.position, transform.rotation);
            db.SetDirection(dir);
            db.transform.Rotate(new Vector3(0, 0, angle));
            db.SetDmg(dmg);
            db.SetShooter(gameObject);
        }
    }
}
