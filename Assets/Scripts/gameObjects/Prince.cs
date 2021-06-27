﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Prince : Hurtable
{
    [SerializeField] float speed;
    private float horizontalInput;
    private float verticalInput;
    private Vector2 moveDir;
    private Vector2 input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(input * speed * Time.deltaTime);
    }


    /*
    private void DetermineSpriteFlip()
    {
        if(transform.localScale.x > 0 && horizontalInput < 0)
        {
            FlipSprite();
        } else if(transform.localScale.x < 0 && horizontalInput > 0)
        {
            FlipSprite();
        }
    }*/

    private void FlipSprite()
    {
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        Vector3 newScale = new Vector3(-x, y, z);
        transform.localScale = newScale;
    }

    /*
    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        moveDir = new Vector2(horizontalInput, verticalInput);
        transform.Translate(moveDir * speed * Time.deltaTime);
    }*/

    public Vector3 GetMoveDir()
    {
        return moveDir;
    }

    public void OnMove(InputAction.CallbackContext context) => input = context.ReadValue<Vector2>();
    
}
