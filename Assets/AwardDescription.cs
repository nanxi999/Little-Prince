using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardDescription : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Prince prince = collision.gameObject.GetComponent<Prince>();
        if(prince)
        {
            animator.SetBool("Bounce", true);
            transform.Find("Description").gameObject.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Prince prince = collision.gameObject.GetComponent<Prince>();
        if (prince)
        {
            animator.SetBool("Bounce", false);
            transform.Find("Description").gameObject.SetActive(false);
        }

    }
}
