using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Prince : Hurtable
{
    [SerializeField] float speed;
    private Vector2 input;
    private GunController gunController;

    Rigidbody2D rb;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gunController = GetComponentInChildren<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(input * speed * Time.deltaTime);
        //rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
        FlipSprite();
        
    }


    private void FlipSprite()
    {
        bool cond1 = input.x > 0 && Mathf.Sign(transform.localScale.x) < 0;
        bool cond2 = input.x < 0 && Mathf.Sign(transform.localScale.x) > 0;
        if (cond1 || cond2)  
        {
            float x = transform.localScale.x;
            float y = transform.localScale.y;
            float z = transform.localScale.z;
            Vector3 newScale = new Vector3(-x, y, z);
            transform.localScale = newScale;
            gunController.Rotate();
        }
    }


    public Vector3 GetMoveDir()
    {
        return input;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(animator != null && gunController != null)
        {
            input = context.ReadValue<Vector2>();
            if (input != Vector2.zero)
            {
                animator.SetBool("IsMoving", true);
                gunController.Rotate();
            }
            else
                animator.SetBool("IsMoving", false);
        }
        
    }


    public void Fire()
    {
        Gun gun = GetComponentInChildren<Gun>();
        if(!gun)
        {
            Debug.Log("player's gun not properly set");
        } else
        {
            gun.Fire();
        }
    }
}
