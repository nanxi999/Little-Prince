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
    private GameObject shooter;
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
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection/newDirection.magnitude;
    }

    public void SetDmg(int newDmg)
    {
        dmg = newDmg;
    }

    public void SetShooter(GameObject tempShooter)
    {
        shooter = tempShooter;
    }

    void DestroyProjectile()
    {
        GameObject obj = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(obj, 2f);
        Destroy(this.gameObject);
    }
}
