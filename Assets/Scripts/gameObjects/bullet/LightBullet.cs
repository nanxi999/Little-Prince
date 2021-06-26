using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int dmg;

    private BoxCollider2D collider;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
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
        if (collision.gameObject.CompareTag("Hurtable"))
        {
            collision.gameObject.GetComponent<Hurtable>().Hurt(dmg);
            Destroy(this.gameObject);
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }
}
