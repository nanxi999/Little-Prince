using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchWizard : Enemy
{
    public Transform firePointBall;     //For laser
    public Transform firePointWand;     //For simple attack
    public float attackRange = 3f;
    public TracerBullet bullet;
    public float attackCd;
    public Laser laserBeam;
    private List<Laser> laserList;
    private bool angry = false;

    private float sinceLastAttack;
    private float curTime;
    private float lastTime = 0;

    protected override void Start()
    {
        base.Start();
        laserList = new List<Laser>();
        GenerateMultipleRays();
    }

    protected override void Update()
    {
        sinceLastAttack += Time.deltaTime;
        base.Update();
        transform.rotation = Quaternion.Euler(-transform.rotation.x, transform.rotation.y, transform.rotation.z);
        FireTrigger();
        UpdateRayWidths();
    }

    public void FireTrigger()
    {
        if (prince == null)
            return;
        if (Vector2.Distance(transform.position, prince.transform.position) < attackRange)
        {
            if (sinceLastAttack < attackCd) { return; }
            else if (!angry)
            {
                StartCoroutine(Freeze(2f));
                animator.SetTrigger("CastTracer");
                sinceLastAttack = 0;
            }
            else
            {
                sinceLastAttack = 0;
                StartCoroutine(Freeze(1));
                animator.SetTrigger("Cast");
            }
        }
    }

    public void FireTracer()
    {
        TracerBullet tempBullet;
        if(bullet)
        {
            tempBullet = Instantiate(bullet, firePointWand.transform.position, transform.rotation);
            tempBullet.SetTarget(prince.transform);
        }
            
    }

    /*
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
    }*/

    public void ChannelRays()
    {
        Vector3 dir = prince.transform.position - firePointBall.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg - 90f;
        laserBeam.SetDir(prince.transform.position - firePointBall.transform.position);
    }

    void GenerateBeam(Vector2 dir)
    {
        Laser laser = Instantiate(laserBeam, transform);
        laser.SetFirePoint(firePointBall);
        laser.SetDir(dir);
        laserList.Add(laser);
    }

    void GenerateMultipleRays()
    {
        GenerateBeam(Vector2.up);
        GenerateBeam(Vector2.down);
        GenerateBeam(Vector2.right);
        GenerateBeam(Vector2.left);

        // 45 degree beams
        float deg = Mathf.Deg2Rad * 45;
        GenerateBeam(new Vector2(1, Mathf.Tan(deg)));
        GenerateBeam(new Vector2(-1, Mathf.Tan(deg)));
        GenerateBeam(new Vector2(1, -Mathf.Tan(deg)));
        GenerateBeam(new Vector2(-1, -Mathf.Tan(deg)));

    }

    protected override void Flip()
    {
        Vector2 newScale = transform.localScale;
        if (newDir.x > 0 && newScale.x > 0)
        {
            newScale.x = -newScale.x;
        }
        else if (newDir.x < 0 && newScale.x < 0)
        {
            newScale.x = -newScale.x;
        }

        transform.localScale = newScale;
    }
    void UpdateRayWidths()
    {
        Debug.Log(curTime + " " + lastTime);
        curTime += Time.deltaTime;

        foreach (Laser laser in laserList)
        {
            if (curTime < 1)
            {
                laser.SetWidth(0.1f);
            }
            else
            {
                if (laser.GetWidth() < 0.7f && curTime - lastTime >= 0.05f)
                {
                    laser.SetWidth(laser.GetWidth() + 0.02f);
                }
            }
        }

        if (curTime - lastTime >= 0.05f)
        {
            lastTime = curTime;
        }
    }

    public override void Hurt(int dmg)
    {
        base.Hurt(dmg);
        Debug.Log(health);
        Debug.Log(max);
        if(health < (max / 2))
        {
            angry = true;
            animator.SetTrigger("Angry");
        }
    }
}
