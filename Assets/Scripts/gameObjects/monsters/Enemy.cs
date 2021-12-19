using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Hurtable
{
    [SerializeField] protected Color slowColor;
    [SerializeField] protected float moveSpeed = 6;
    [SerializeField] protected float targetSwitchThreshold = 30;
    [SerializeField] protected SpriteRenderer shadowRenderer;


    public float dmg = 1;
    protected bool freeze = false;

    public Prince prince;
    protected LevelController levelController;
    protected Vector2 newDir;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected bool isAlive = true;
    private float speedModifier = 1;
    protected bool isSLowed;
    protected AIPath path;
    protected bool selected;

    protected override void Awake()
    {
        base.Awake();
        levelController = FindObjectOfType<LevelController>();
        levelController.EnemySpawned();
    }

    protected virtual void Start()
    {
        prince = FindObjectOfType<Prince>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        path = GetComponent<AIPath>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateTarget();
        Move();
        Flip();
    }

    
    protected virtual void Move()
    {
        if (freeze)
        {
            return;
        }

        if (path)
        {
            path.maxSpeed = moveSpeed * speedModifier;
        }
        if (!prince)
        {
            Prince[] princes = FindObjectsOfType<Prince>();
            float minDist = float.MaxValue;
            foreach (Prince p in princes)
            {
                float dist = Vector2.Distance(transform.position, p.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    prince = p;
                }
            }

        }
        else
        {
            newDir = Vector2.zero;
            //transform.position = Vector3.MoveTowards(transform.position, prince.transform.position, moveSpeed * Time.deltaTime);


            if (prince.transform.position.x > transform.position.x)
            {
                newDir.x = 1;
            }
            else
            {
                newDir.x = -1;
            }

            if (prince.transform.position.y > transform.position.y)
            {
                newDir.y = 1;
            }
            else
            {
                newDir.y = -1;
            }
            //rb.MovePosition(rb.position + newDir * moveSpeed * speedModifier * Time.fixedDeltaTime);
        }

    }

    protected virtual void Flip()
    {       
        Vector2 newScale = transform.localScale;
        if (newDir.x > 0 && newScale.x < 0)
        {
            newScale.x = -newScale.x;
        }
        else if (newDir.x < 0 && newScale.x > 0)
        {
            newScale.x = -newScale.x;
        }

        transform.localScale = newScale;
    }

    public override void Hurt(float dmg)
    {
        health -= dmg;
        animator.SetBool("Hurt", true);
        if (health <= 0)
        {
            if(isAlive)
            {
                levelController.EnemyKilled();
                isAlive = false;
            } else
            {
                return;
            }
            OnDie();
            Destroy(this.gameObject);
        }
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public float GetDmg()
    {
        return dmg;
    }

    public void SetDmg(float new_dmg)
    {
        dmg = new_dmg;
    }

    public virtual void PushBack(Vector3 pushBackVelocity, float duration)
    {
        if(pushBackVelocity != Vector3.zero) 
            rb.velocity = pushBackVelocity;
        StartCoroutine(PushBackFreeze(duration));
    }

    protected virtual IEnumerator PushBackFreeze(float duration)
    {
        freeze = true;
        GetComponent<AIPath>().enabled = false;
        yield return new WaitForSeconds(duration);
        GetComponent<AIPath>().enabled = true;
        //animator.SetBool("Hurt", false);
        freeze = false;
    }

    protected virtual IEnumerator Freeze(float duration)
    {
        freeze = true;
        GetComponent<AIPath>().enabled = false;
        yield return new WaitForSeconds(duration);
        GetComponent<AIPath>().enabled = true;
        freeze = false;
    }

    protected virtual void ChangeRendererColor(Color color)
    {
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        if(renderer)
        {
            renderer.color = color;
        }
    }

    public void IceAttackHit(float duration, float slowAmount)
    {
        if(isSLowed)
        {
            return;
        }
        ChangeRendererColor(slowColor);
        if(slowAmount >= 0 && slowAmount <= 1)
        {
            speedModifier = slowAmount;
            isSLowed = true;
        } else
        {
            return;
        }
        StartCoroutine(unFreeze(duration));
    }

    private IEnumerator unFreeze(float duration)
    {
        yield return new WaitForSeconds(duration);
        speedModifier = 1f;
        ChangeRendererColor(Color.white);
        isSLowed = false;

    }

    // choose the target prince based on distance.
    public void UpdateTarget()
    {
        Prince[] princes = FindObjectsOfType<Prince>();
        float minDist = float.MaxValue;
        float curDist = float.MaxValue;
        if(prince)
        {
            curDist = Vector2.Distance(transform.position, prince.transform.position);
        }
        Prince tempPrince = new Prince();

        foreach(Prince newPrince in princes)
        {
            if (newPrince != prince && !newPrince.IsCryin())
            {
                float distance = Vector2.Distance(transform.position, newPrince.transform.position);
                if(distance < minDist)
                {
                    minDist = distance;
                    tempPrince = newPrince;
                }
            }
        }
        if(!tempPrince) { return; }

        if ((prince && prince.IsCryin()) || curDist - Vector2.Distance(transform.position,tempPrince.transform.position) > targetSwitchThreshold)
        {
            prince = tempPrince;
        }
    }

    public void SelectAsTarget(bool status)
    {
        if(shadowRenderer)
        {
            if (status)
            {
                shadowRenderer.color = Color.red;
            }
            else
            {
                shadowRenderer.color = Color.black;
            }
        }
    }
}
