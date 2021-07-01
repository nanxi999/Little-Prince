using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    CircleCollider2D circle;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    private LayerMask layers;
    Mutant mutant;

    void Start()
    {
        mutant = GetComponentInParent<Mutant>();
        animator = GetComponent<Animator>();
        circle = GetComponent<CircleCollider2D>();

        // find children "attackPoint"
        attackPoint =  transform.Find("AttackPoint");

        // set mask
        layers = LayerMask.GetMask("HittableObject");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Prince prince = collision.gameObject.GetComponent<Prince>();
       if(prince) 
        {
            mutant.ToggleFreeze();
            animator.SetTrigger("Attack");
        }
    }

    private void CheckDamage()
    {
        mutant.ToggleFreeze();
        if(attackPoint)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layers);

            foreach(Collider2D enemy in hitEnemies)
            {
                Prince prince = enemy.gameObject.GetComponent<Prince>();
                if(prince)
                {
                    prince.Hurt(mutant.GetDmg());
                }
            }
        } else
        {
            Debug.Log("The attack point for mutant is not set");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
