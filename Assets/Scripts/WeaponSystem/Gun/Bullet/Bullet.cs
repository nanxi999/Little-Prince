using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    [SerializeField] private float lifeTime;
    [SerializeField] private Sprite[] sprites;
    public GameObject destroyEffect;
    public float pushBackForce;
    public string[] layers;

    protected float dmg;
    protected bool iceBullet;
    protected GameObject shooter;
    protected Vector3 direction;
    protected Vector3 initPosition;
    protected bool isColliding = false;
    protected List<int> layerIndexes;

    private SpriteRenderer renderer;

    protected virtual void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        initPosition = transform.position;
        renderer = GetComponentInChildren<SpriteRenderer>();
        InitIndexList();
        iceBullet = false;
    }

    void Update()
    {
        Fly();
        //UpdateSpriteRenderer();
    }

    public void InitIndexList()
    {
        layerIndexes = new List<int>();
        foreach (string layerName in layers)
        {
            int index = LayerMask.NameToLayer(layerName);
            layerIndexes.Add(index);
        }
    }

    public void SetDmg(float newDmg)
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

    protected virtual void Fly()
    {
        Vector3 tempDir = new Vector3(0, -1f, 0);
        transform.Translate(tempDir * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(dmg);
        int res = LayerMask.GetMask(layers);
        GameObject hitObject = collision.gameObject;
        if (!(layerIndexes.Contains(collision.gameObject.layer)) || isColliding)
            return;
        isColliding = true;

        Enemy enemy = hitObject.GetComponent<Enemy>();
        if (hitObject != shooter)
        {
            Vector3 dir = transform.position - initPosition;
            if (enemy != null && dir != null)
            {
                if (dir.magnitude == 0) { return; }
                else
                {
                    enemy.PushBack(dir / dir.magnitude * pushBackForce, 0.7f);
                }
            }
            if (hitObject.GetComponent<Hurtable>())
            {
                hitObject.GetComponent<Hurtable>().Hurt(dmg);
                if(iceBullet && enemy)
                {
                    hitObject.GetComponent<Enemy>().IceAttackHit(5f, 0.4f);
                }
            }
            DestroyProjectile();
        }
    }

    public void SetShooter(GameObject tempShooter)
    {
        shooter = tempShooter;
    }

    public GameObject GetShooter()
    {
        return shooter;
    }

    public void ToggleIceBullet(bool status)
    {
        iceBullet = status;
    }

    private void UpdateSpriteRenderer()
    {
        if(sprites.Length < 2)
        {
            Debug.Log("Please set the sprites for bullets");
            return;
        }
        if (iceBullet)
        {
            renderer.sprite = sprites[1];
        }
        else
        {
            renderer.sprite = sprites[0];
        }
    }
}
