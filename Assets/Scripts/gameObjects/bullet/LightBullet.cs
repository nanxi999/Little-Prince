using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int dmg;
    [SerializeField] float lifeTime;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] float pushBackForce;

    private BoxCollider2D collider;
    private Vector3 direction;
    private Vector3 initPosition;
    private bool isColliding = false;

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

    private void Fly()
    {
        Vector3 tempDir = new Vector3(0, -1f, 0);
        transform.Translate(tempDir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    void DestroyProjectile()
    {
        GameObject obj = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(obj, 2f);
        Destroy(this.gameObject);
    }
}
