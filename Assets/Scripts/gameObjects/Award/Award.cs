using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("entered");
        if (collision.gameObject.GetComponent<Bullet>())
        {
            Destroy(collision.gameObject);
            GiveAwards();
            DestroyThis();
        }
    }

    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    protected virtual void GiveAwards() { }
}
