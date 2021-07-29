using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Pathfinding;
public class ArchWizard : Enemy
{
    public float dashDamage;
    public float dashSpeed;
    public float dashPushBack;
    public Transform firePointBall;     //For laser
    public Transform firePointWand;     //For simple attack
    public float attackRange = 3f;
    public TracerBullet bullet;
    public float attackCd;
    public Laser laserBeam;
    private List<Laser> laserList;
    private bool angry = false;
    private bool isDashing = false;

    private float sinceLastAttack;
    private int attackCount = 0;
    private int laserThreshold = 2;
    private LayerMask raycastMask;
    private Transform raycastStartingPoint;
    private Vector2 dashDir;
    
    protected override void Start()
    {
        base.Start();
        laserList = new List<Laser>();
        //GenerateMultipleRays();
        GetComponent<AIPath>().maxSpeed = moveSpeed;
        Physics2D.queriesStartInColliders = false;
        raycastMask = LayerMask.GetMask("Friendly", "BackgroundHittable");
        raycastStartingPoint = firePointWand.transform;
        GetComponent<AIPath>().maxSpeed = moveSpeed;
    }

    protected override void Update()
    {
        sinceLastAttack += Time.deltaTime;
        GetComponent<AIDestinationSetter>().target = prince.transform;
        base.Update();
        transform.rotation = Quaternion.Euler(-transform.rotation.x, transform.rotation.y, transform.rotation.z);
        FireTrigger();
        Dash();
        DrawRay();
    }

    public void DrawRay()
    {
        Vector2 dir = prince.transform.position - transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, dir, attackRange, raycastMask.value);
        
        if(hitInfo.collider)
        {
            Debug.DrawLine(raycastStartingPoint.position, hitInfo.point, Color.red);
        } else
        {
            Debug.DrawLine(raycastStartingPoint.position, prince.transform.position, Color.green);
        }
    }

    public void FireTrigger()
    {
        if (prince == null || sinceLastAttack < attackCd)
            return;

        Vector2 dir = prince.transform.position - raycastStartingPoint.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(raycastStartingPoint.position, dir, attackRange, raycastMask.value);
        if (hitInfo.transform.GetComponent<Prince>())
        {
            if (!angry)
            {
                StartCoroutine(Freeze(2f));
                animator.SetTrigger("CastTracer");
                sinceLastAttack = 0;
            }
            else
            {
                if (attackCount >= laserThreshold)
                {
                    sinceLastAttack = 0;
                    StartCoroutine(Freeze(4f));
                    animator.SetTrigger("CastLaser");
                    attackCount = 0;
                    laserThreshold = Random.Range(1, 4);
                }
                else
                {
                    sinceLastAttack = 0;
                    StartCoroutine(Freeze(4f));
                    animator.SetTrigger("Dash");
                    attackCount += 1;
                }
            }
        }          
    }

    public void SetDashDir()
    {
        Vector2 vec = prince.transform.position - transform.position;
        dashDir = vec / vec.magnitude;
    }

    void Dash()
    {
        if(isDashing)
        {
            transform.Translate(dashDir * dashSpeed * Time.deltaTime);
        }
    }

    public void StopDash()
    {
        isDashing = false;
    }

    public void StartDash()
    {
        isDashing = true;
    }

    public void FireTracer()
    {
        TracerBullet tempBullet;
        if(bullet)
        {
            tempBullet = Instantiate(bullet, firePointWand.transform.position, transform.rotation);
            tempBullet.SetDmg(dmg);
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

    public void StartBloom()
    {
        FindObjectOfType<Volume>().enabled = true;
    }

    public void EndBloom()
    {
        FindObjectOfType<Volume>().enabled = false;
    }

    void GenerateMultipleRays()
    {
        firePointBall.Find("Start").gameObject.SetActive(true);
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

        // Update ray width
        StartCoroutine(RayWidthsGrow());
    }

    void DestroyMultipleRays()
    {
        StartCoroutine(RayWidthsFade());
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

    IEnumerator RayWidthsGrow()
    {
        foreach (Laser laser in laserList)
        {
            laser.SetWidth(0.1f);
        }    
        yield return new WaitForSeconds(0.5f);
        while (laserList[0].GetWidth() < 0.7f)
        {
            foreach (Laser laser in laserList)
            {
                laser.SetWidth(laser.GetWidth() + 0.02f);
            }
            yield return new WaitForSeconds(0.05f);
        } 
    }

    IEnumerator RayWidthsFade()
    {
        while (laserList[0].GetWidth() >= 0.1)
        {
            foreach (Laser laser in laserList)
            {
                laser.SetWidth(laser.GetWidth() - 0.05f);
            }
            yield return new WaitForSeconds(0.05f);
        }
        foreach (Laser laser in laserList)
        {
            laser.DestroyHitEffect();
            Destroy(laser.gameObject);
        }
        firePointBall.Find("Start").gameObject.SetActive(false);
        laserList.Clear();
    }

    /*
    void UpdateRayWidthsGrow()
    {
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
    }*/

    public override void Hurt(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Debug.Log(gameObject.name);
            if (isAlive)
            {
                levelController.EnemyKilled();
                isAlive = false;
            }
            else
            {
                return;
            }
            if (deathEffect)
            {
                GameObject obj = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(obj, 3f);
            }
            else
            {
                Debug.Log("death VFX not set");
            }
            foreach (Laser laser in laserList)
            {
                laser.DestroyHitEffect();
                Destroy(laser.gameObject);
            }
            Destroy(this.gameObject);
        }
        if (health < (max / 2))
        {
            angry = true;
            AIPath aipath = GetComponent<AIPath>();
            aipath.maxSpeed = aipath.maxSpeed * 1.5f;
            raycastStartingPoint = transform;
            animator.SetTrigger("Angry");
        }
    }

    protected override void ChangeRendererColor(Color color)
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>(true);
        foreach(SpriteRenderer renderer in renderers)
        {
            renderer.color = color;
        }
    }
}
