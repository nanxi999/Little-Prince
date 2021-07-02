using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : Hurtable
{
    [SerializeField] int dmg = 1;
    [SerializeField] float moveSpeed = 3;

    Prince prince;
    LevelController levelController;
    Vector2 newDir;
    bool freeze = false;
    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        pushBackable = true;
        prince = FindObjectOfType<Prince>();
        levelController = FindObjectOfType<LevelController>();
        levelController.EnemySpawned();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
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

            
            if(prince.transform.position.x > transform.position.x)
            {
                newDir.x = 1;
            } else
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
            rb.MovePosition(rb.position + newDir *  moveSpeed * Time.fixedDeltaTime);
        }

    }

    public void Flip()
    {
        Vector2 newScale = transform.localScale;
        if(newDir.x > 0 && newScale.x < 0)
        {
            newScale.x = -newScale.x;
        } else if(newDir.x < 0 && newScale.x > 0)
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
            {                GameObject obj = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(obj, 3f);
            }
            else
            {
                Debug.Log("death VFX not set");
            }
            Destroy(this.gameObject);
        }
    }

    public int GetDmg()
    {
        return dmg;
    }

    public void SetDmg(int new_dmg)
    {
        dmg = new_dmg;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void ToggleFreeze()
    {
        freeze = !freeze;
    }

    public void PushBack(Vector3 pushBackVelocity, float duration)
    {
        rb.velocity = pushBackVelocity;
        StartCoroutine(Freeze(duration));
    }

    IEnumerator Freeze(float duration)
    {
        freeze = true;
        yield return new WaitForSeconds(duration);
        animator.SetBool("Hurt", false);
        freeze = false;
    }
}
