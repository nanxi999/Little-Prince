using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    [SerializeField] int health;

    // Start is called before the first frame update
    void Start()
    {
    }

    public virtual void Hurt(int dmg)
    {
        health -= dmg;
        if(health < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public bool ifAlive()
    {
        return health >= 0;
    }

}
