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

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gunController = GetComponentInChildren<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(input * speed * Time.deltaTime);
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
        input = context.ReadValue<Vector2>();
        if(input != Vector2.zero)
        {
            animator.SetBool("IsMoving", true);
            Debug.Log(input);
            gunController.Rotate();
        }
        else
            animator.SetBool("IsMoving", false);
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
