using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Prince : Hurtable
{
    [SerializeField] float speed;
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

    public void OnMove(InputAction.CallbackContext context) => input = context.ReadValue<Vector2>();
    

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
