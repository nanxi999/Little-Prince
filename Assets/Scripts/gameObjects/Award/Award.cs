using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award : MonoBehaviour
{
    Animator animator;
    LevelController levelConroller;
    bool isCollected = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        levelConroller = FindObjectOfType<LevelController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if(bullet & !isCollected)
        {
            isCollected = true;
            Prince prince = bullet.GetShooter().GetComponent<Prince>();
            if(prince && !(levelConroller.PlayerIsAwarded(prince.GetID())))
            {
                if (collision.gameObject.GetComponent<Bullet>())
                {
                    Destroy(collision.gameObject);
                    GiveAwards(bullet.GetShooter());
                    levelConroller.PlayerAwarded(prince.GetID());
                    DestroyThis();
                }
            }
            
        }
        
    }

    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    protected virtual void GiveAwards(GameObject prince) { }
}
