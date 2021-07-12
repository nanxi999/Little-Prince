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
    public string[] layers;

    protected GameObject shooter;
    protected Vector3 direction;
    protected Vector3 initPosition;
    protected bool isColliding = false;
    protected List<int> layerIndexes;

    protected virtual void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        initPosition = transform.position;
        InitIndexList();
    }

    void Update()
    {
        Fly();
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

    protected virtual void Fly()
    {
        Vector3 tempDir = new Vector3(0, -1f, 0);
        transform.Translate(tempDir * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        int res = LayerMask.GetMask(layers);
        Debug.Log("yasssssssssssssssssssewiuroiewuroi");
        GameObject hitObject = collision.gameObject;
        if (!(layerIndexes.Contains(collision.gameObject.layer)) || isColliding) 
            return;
        isColliding = true;
        
        Enemy enemy = hitObject.GetComponent<Enemy>();
        if (hitObject != shooter)
        {
            Vector3 dir = transform.position - initPosition;
            if(enemy!=null && dir!=null)
            {
                if(dir.magnitude == 0) { return; } 
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
}
