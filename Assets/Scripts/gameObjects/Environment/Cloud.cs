using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("entered");
        if(collision.gameObject.GetComponent<Bullet>())
        {
            Destroy(collision.gameObject);
            animator.SetTrigger("Hit");
            GiveAwards();
        }
    }

    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    private void GiveAwards()
    {

    }
}
