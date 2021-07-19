using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    [SerializeField] protected float health;
    public GameObject deathEffect;
    protected float max;

    // Start is called before the first frame update

    protected virtual void Awake()
    {
        max = health;
    }

    public virtual void Hurt(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            if(deathEffect)
            {
                Debug.Log("created effect");
                GameObject obj = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(obj, 3f);
            } else
            {
                Debug.Log("death VFX not set");
            }
            Destroy(this.gameObject);
        }
    }

    public bool ifAlive()
    {
        return health >= 0;
    }

    public void SetHealth(float val)
    {
        health = val;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return max;
    }

    public void SetMaxHealth(float val)
    {
        max = val;
    }

}
