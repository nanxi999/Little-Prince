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
    
    private int infBulletsLevelsLeft = 0;
    private bool receiveInput = true;
    private Vector2 input;
    private InputAction fireAction;
    private bool firing = false;
    private bool switching = false;
    private TMP_Text weaponStat;
    private int playerID;
    private bool freeze = false;
    private StatsManager stats;

    // variables for the death and save features
    private float saveTime = 2f;
    private bool cryin = false;
    private float remainingSaveTime;
    private bool saveKeyPressed = false;
    private InputAction saveAction;
    private Prince princeToSave;
    private Enemy target;

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
        stats = GetComponent<StatsManager>();

        var playerInput = GetComponent<PlayerInput>();
        var map = playerInput.currentActionMap;
        fireAction = map.FindAction("Fire");
        fireAction.started += ToggleFiring;
        fireAction.canceled += ToggleFiring;

        saveAction = map.FindAction("Save");
        saveAction.started += ToggleSavin;
        saveAction.canceled += ToggleSavin;

        Debug.Log(playerID);
    }

    // Update is called once per frame
    void Update()
    {
        if(!freeze)
        {
            transform.Translate(input * speed * stats.GetMoveSpeedFactor() * Time.deltaTime);
            FlipSprite();
            Fire();
            SaveTargetPrince();
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

    public void ToggleSavin(InputAction.CallbackContext context)
    {
        saveKeyPressed = !saveKeyPressed;
    }

    public void ToggleFiring(InputAction.CallbackContext context)
    {
        firing = !firing;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(receiveInput && !freeze && !cryin)
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
           
        } else
        {
            input = Vector2.zero;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Prince collidedPrince = collision.gameObject.GetComponent<Prince>();
        if(collidedPrince && collidedPrince.IsCryin())
        {
            princeToSave = collidedPrince;
        } 
    }

    public override void Hurt(float dmg)
    {
        if(cryin)
        {
            return;
        }
        health -= dmg * stats.GetHurtFactor();
        if (health <= 0)
        {
            health = 0;
            if (deathEffect)
            {
                GameObject obj = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(obj, 3f);
            }
            else
            {
                Debug.Log("death VFX not set");
            }

            if(animator)
            {
                // start cryin and waiting for help
                animator.SetTrigger("Cry");
                cryin = true;
                FindObjectOfType<LevelController>().GameOverCheck();
                remainingSaveTime = saveTime;
            }
        }
    }

    public bool IsCryin()
    {
        return cryin;
    }

    public void SaveTargetPrince()
    {
        if(princeToSave)
        {
            if(princeToSave.IsCryin())
            {
                if(saveKeyPressed)
                {
                    princeToSave.Save();
                }
            } else
            {
                princeToSave = null;
            }
        }
    }

    public void Save()
    {
        if(remainingSaveTime > 0)
        {
            remainingSaveTime -= Time.deltaTime;
        } else
        {
            // the target prince is saved
            animator.SetTrigger("Saved");
            saveTime += 1.5f;
            cryin = false;
            health = max;
            
        }
    }

    public float GetRemainSaveTime()
    {
        return remainingSaveTime;
    }

    public float GetSaveTime()
    {
        return saveTime;
    }

}
