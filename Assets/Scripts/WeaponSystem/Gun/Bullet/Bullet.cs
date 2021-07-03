using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int dmg;
    public float lifeTime;
    public GameObject destroyEffect;
    public float pushBackForce;

    protected BoxCollider2D collider;
    protected Vector3 direction;
    protected Vector3 initPosition;
    protected bool isColliding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDmg(int newDmg)
    {
        dmg = newDmg;
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    protected void DestroyProjectile()
    {
        GameObject obj = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(obj, 2f);
        Destroy(this.gameObject);
    }

    protected void Fly()
    {
        Vector3 tempDir = new Vector3(0, -1f, 0);
        transform.Translate(tempDir * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding)
            return;
        isColliding = true;
        GameObject hitObject = collision.gameObject;
        Enemy enemy = hitObject.GetComponent<Enemy>();
        if (hitObject.CompareTag("Hurtable"))
        {
            Vector3 dir = transform.position - initPosition;
            if(enemy!=null && dir!=null)
                enemy.PushBack(dir/dir.magnitude * pushBackForce, 0.7f);
            hitObject.GetComponent<Hurtable>().Hurt(dmg);
            DestroyProjectile();
        }
    }
}
