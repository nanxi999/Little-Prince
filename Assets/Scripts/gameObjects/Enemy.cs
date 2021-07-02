using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Hurtable
{
    public float moveSpeed = 3;

    protected bool freeze = false;

    public Prince prince;
    protected LevelController levelController;
    protected Vector2 newDir;
    protected Rigidbody2D rb;
    protected Animator animator;

    protected virtual void Start()
    {
        prince = FindObjectOfType<Prince>();
        levelController = FindObjectOfType<LevelController>();
        levelController.EnemySpawned();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Flip();
    }

    private void Move()
    {
        if (freeze)
        {
            return;
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
            Vector2 targetPosition = Vector2.MoveTowards(rb.position, prince.transform.position, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(targetPosition);
        }

    }

    public void Flip()
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

    public override void Hurt(int dmg)
    {
        health -= dmg;
        animator.SetBool("Hurt", true);
        if (health <= 0)
        {
            levelController.EnemyKilled();
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

    public void PushBack(Vector3 pushBackVelocity, float duration)
    {
        rb.velocity = pushBackVelocity;
        StartCoroutine(PushBackFreeze(duration));
    }

    IEnumerator PushBackFreeze(float duration)
    {
        freeze = true;
        yield return new WaitForSeconds(duration);
        animator.SetBool("Hurt", false);
        rb.velocity = Vector3.zero;
        freeze = false;
    }
}
