using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    [SerializeField] protected int health;
    public GameObject deathEffect;
    protected int max;

    // Start is called before the first frame update

    protected virtual void Awake()
    {
        max = health;
    }

    public virtual void Hurt(int dmg)
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

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return max;
    }

}
