using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{  
    [SerializeField] private Sprite[] sprites;
    public GameObject destroyEffect;
    public string[] layers;

    protected float speed;
    protected float dmg;
    protected float lifeTime;
    protected float pushBackForce;
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
    }

    protected virtual void Update()
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

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetLifeTime(float newLifeTime)
    {
        lifeTime = newLifeTime;
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void SetPushBackForce(float newPushBackForce)
    {
        pushBackForce = newPushBackForce;
    }

    protected virtual void DestroyProjectile()
    {
        if(destroyEffect)
        {
            GameObject obj = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(obj, 1f);
        }
        Destroy(this.gameObject);
    }

    protected virtual void Fly()
    {
        Vector3 tempDir = new Vector3(0, -1f, 0);
        transform.Translate(tempDir * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        int res = LayerMask.GetMask(layers);
        GameObject hitObject = collision.gameObject;
        if (hitObject != shooter)
        {
            if (!(layerIndexes.Contains(collision.gameObject.layer)) || isColliding)
                return;
            isColliding = true;
            Enemy enemy = hitObject.GetComponent<Enemy>();

            Vector3 dir = transform.position - initPosition;
            if (enemy != null && dir != null)
            {
                if (dir.magnitude == 0) { }
                else
                {
                    enemy.PushBack(dir / dir.magnitude * pushBackForce, 0.7f);
                }
            }
            if (hitObject.GetComponent<Hurtable>())
            {
                hitObject.GetComponent<Hurtable>().Hurt(dmg);
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

    /*
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
    }*/
}
