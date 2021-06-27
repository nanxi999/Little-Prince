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
    // Start is called before the first frame update
    void Start()
    {
        prince = FindObjectOfType<Prince>();
        levelController = FindObjectOfType<LevelController>();
        levelController.EnemySpawned();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(!prince) { 
           Prince[] princes= FindObjectsOfType<Prince>();
            float minDist = float.MaxValue;     
            foreach (Prince p in princes)
            {
                float dist = Vector2.Distance(transform.position, p.transform.position);
                if( dist < minDist)
                {
                    minDist = dist;
                    prince = p;
                }
            }

        } else
        {
            float step = Time.deltaTime * moveSpeed;
            transform.position = Vector3.MoveTowards(transform.position, prince.transform.position, step);
        }

    }

    public override void Hurt(int dmg)
    {
        levelController.EnemyKilled();
        base.Hurt(dmg);
    }

}
