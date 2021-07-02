using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int dmg;
    [SerializeField] float lifeTime;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] float pushBackForce;

    private BoxCollider2D collider;
    private Vector3 direction;
    private Vector3 initPosition;

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
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hitObject = collision.gameObject;
        Enemy enemy = hitObject.GetComponent<Enemy>();
        if (hitObject.CompareTag("Hurtable"))
        {
            Vector3 dir = transform.position - initPosition;
            if (enemy)
                enemy.PushBack(dir / dir.magnitude * pushBackForce, 0.7f);
            hitObject.GetComponent<Hurtable>().Hurt(dmg);
            DestroyProjectile();
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection/newDirection.magnitude;
    }

    void DestroyProjectile()
    {
        GameObject obj = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(obj, 2f);
        Destroy(this.gameObject);
    }
}
