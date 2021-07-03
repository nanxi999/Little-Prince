using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBullet : Bullet
{
    private GameObject shooter;

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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding)
            return;
        isColliding = true;
        GameObject hitObject = collision.gameObject;
        if (hitObject.CompareTag("Hurtable") && hitObject != shooter)
        {
            Vector3 dir = transform.position - initPosition;
            hitObject.GetComponent<Hurtable>().Hurt(dmg);
            DestroyProjectile();
        }
    }

    public void SetShooter(GameObject tempShooter)
    {
        shooter = tempShooter;
    }
}
