using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchWizard : Enemy
{
    public Transform attackPoint;
    public float attackRange = 3f;
    public DarkBullet darkBullet;
    public float attackCd;
    public Laser laserBeam;

    private float sinceLastAttack;

    protected override void Update()
    {
        sinceLastAttack += Time.deltaTime;
        base.Update();
        transform.rotation = Quaternion.EulerRotation(-transform.rotation.x, transform.rotation.y, transform.rotation.z);
        FireTrigger();
        ChannelRays();
    }

    public void FireTrigger()
    {
        if (Vector2.Distance(transform.position, prince.transform.position) < attackRange)
        {
            if (sinceLastAttack < attackCd) { return; }
            else
            {
                sinceLastAttack = 0;
                StartCoroutine(Freeze(1));
                animator.SetTrigger("Cast");
            }
        }
    }

    public void Fire()
    {
        if (darkBullet)
        {
            Vector3 dir = prince.transform.position - attackPoint.transform.position;
            float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg - 90f;
            DarkBullet db = Instantiate(darkBullet, attackPoint.transform.position, transform.rotation);
            db.SetDirection(dir);
            db.transform.Rotate(new Vector3(0, 0, angle));
            db.SetDmg(dmg);
            db.SetShooter(gameObject);
        }
    }

    public void ChannelRays()
    {
        Vector3 dir = prince.transform.position - attackPoint.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg - 90f;
        laserBeam.SetDir(prince.transform.position - attackPoint.transform.position);
    }
}
