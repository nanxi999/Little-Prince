﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        collider = GetComponent<BoxCollider2D>();
        initPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
    }


}