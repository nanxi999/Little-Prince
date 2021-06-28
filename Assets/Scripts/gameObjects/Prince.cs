using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Prince : Hurtable
{
    [SerializeField] float speed;
    private Vector2 input;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(input * speed * Time.deltaTime);
        transform.Translate(input * speed * Time.deltaTime);
    }


    private void FlipSprite()
    {
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        Vector3 newScale = new Vector3(-x, y, z);
        transform.localScale = newScale;
    }


    public Vector3 GetMoveDir()
    {
        return input;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        if(input != Vector2.zero)
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);
    }


    public void Fire()
    {
        Debug.Log("start firing");
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
