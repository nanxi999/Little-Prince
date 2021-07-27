using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Hurtable
{
    [SerializeField] protected Color slowColor;
    [SerializeField] protected float moveSpeed = 6;
    public int dmg = 1;
    protected bool freeze = false;

    public Prince prince;
    protected LevelController levelController;
    protected Vector2 newDir;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected bool isAlive = true;
    private float speedModifier = 1;
    protected bool isSLowed;
    private AIPath path;

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
            Debug.Log(gameObject.name);
            if(isAlive)
            {
                levelController.EnemyKilled();
                isAlive = false;
            } else
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
            Destroy(this.gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public int GetDmg()
    {
        return dmg;
    }

    public void SetDmg(int new_dmg)
    {
        dmg = new_dmg;
    }

    public virtual void PushBack(Vector3 pushBackVelocity, float duration)
    {
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
}
