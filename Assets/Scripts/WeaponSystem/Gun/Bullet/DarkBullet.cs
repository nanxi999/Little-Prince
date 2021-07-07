using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBullet : Bullet
{

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        collider = GetComponent<BoxCollider2D>();
        initPosition = transform.position;
        InitIndexList();
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
    }
}
