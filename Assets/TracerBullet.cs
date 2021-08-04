﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TracerBullet : Bullet
{
    public Transform target;
    [SerializeField] private string element;
    [SerializeField] RocketExplosion explosionVFX;

    private AIDestinationSetter dest;
    private AIPath path;
    private Vector2 dir;

    protected override void Start()
    {
        dest = GetComponent<AIDestinationSetter>();
        path = GetComponent<AIPath>();
        if (path && dest)
        {
            InitAIPath();
        }
        base.Start();
        InitDir();
    }

    private void InitDir()
    {
        Vector2 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg ;
        transform.Rotate(new Vector3(0, 0, angle));
    }

    private void InitAIPath()
    {
        path.maxSpeed = speed + 13;
        dest.target = target;
        lifeTime = 2.5f;
    }

    public void SetTarget(Transform tempTarget)
    {
        target = tempTarget;
    }

    protected override void Fly()
    {
        if(!path)
        {
            if(element.Equals("Darkness") || element == null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            } else if(element.Equals("Explosion"))
            {
                dir = Vector2.left * 2.3f;
                transform.Translate(dir * speed * Time.deltaTime);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != shooter)
        {
            OnContact();
        }

        if(element.Equals("Ice"))
        {
            if(collision.gameObject.GetComponent<Prince>())
            {
                dmg = 5;
                collision.gameObject.GetComponent<Prince>().Freeze(0.5f, 4);
            }
        }

        if(element.Equals("Darkness"))
        {
            if (collision.gameObject.GetComponent<Prince>())
            {
                collision.gameObject.GetComponentInChildren<Prince>().DarkBoltHit(2.0f, 4);
            }
        }
        base.OnTriggerEnter2D(collision);
    }

    private void OnContact()
    {
        if(element.Equals("Explosion"))
        {
            RocketExplosion exp = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            exp.SetDmg((int)10);
            Destroy(exp, 1f);
        } else if(element.Equals("Ice"))
        {

        } 
    }
}
