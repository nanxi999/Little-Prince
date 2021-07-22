using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Prince : Hurtable
{
    public float speed;
    public GameObject controllerObj;
    public Gun initialGun;
    public GunController gunController;

    private bool receiveInput = true;
    private Vector2 input;
    private InputAction fireAction;
    private bool firing = false;
    private bool switching = false;
    private TMP_Text weaponStat;
    private int playerID;
    private bool freeze = false;

    Rigidbody2D rb;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controllerObj = Instantiate(controllerObj, transform);
        gunController = controllerObj.GetComponent<GunController>();
        weaponStat = GetComponentInChildren<TMP_Text>();
        playerID = FindObjectsOfType<Prince>().Length;
        controllerObj.transform.parent = transform.Find("Character").transform;

        var playerInput = GetComponent<PlayerInput>();
        var map = playerInput.currentActionMap;
        fireAction = map.FindAction("Fire");
        fireAction.started += ToggleFiring;
        fireAction.canceled += ToggleFiring;
    }

    // Update is called once per frame
    void Update()
    {
        if(!freeze)
        {
            transform.Translate(input * speed * Time.deltaTime);
            //rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
            FlipSprite();
            Fire();
            weaponStat.text = gunController.GetGunStat();
        }
    }

    private void FlipSprite()
    {
        Transform character = transform.Find("Character");
        bool cond1 = input.x > 0 && Mathf.Sign(character.localScale.x) < 0;
        bool cond2 = input.x < 0 && Mathf.Sign(character.localScale.x) > 0;
        if (cond1 || cond2)  
        {
            float x = character.localScale.x;
            float y = character.localScale.y;
            float z = character.localScale.z;
            Vector3 newScale = new Vector3(-x, y, z);
            character.localScale = newScale;
            gunController.Rotate();
        }
    }


    public Vector3 GetMoveDir()
    {
        return input;
    }

    
    public void ToggleFiring(InputAction.CallbackContext context)
    {
        firing = !firing;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(receiveInput && !freeze)
        {
            if (animator != null && gunController != null)
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
        
    }


    public void Fire()
    {
        if(receiveInput && firing)
        {
            Gun gun = gunController.GetComponentInChildren<Gun>();
            if (!gun)
            {
                Debug.Log("player's gun not properly set");
            }
            else
            {
                gun.Fire();
            }
        } else
        {
        }
    }

    public void SwitchGun(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            gunController.SwitchNextGun();
        }
        
    }

    public void ToggleInputStatus()
    {
        receiveInput = !receiveInput;
    }

    public int GetID()
    {
        return playerID;
    }

    IEnumerator PushBackFreeze(float time)
    {
        freeze = true;
        yield return new WaitForSeconds(time);
        freeze = false;
    }

    public virtual void PushBack(Vector3 pushBackVelocity, float duration)
    {   
        rb.velocity = pushBackVelocity;
        StartCoroutine(PushBackFreeze(duration));
    }
}
