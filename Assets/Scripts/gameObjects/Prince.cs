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
    private int playerID;
    private bool freeze = false;
    private StatsManager stats;
    [SerializeField] private TMP_Text weaponStat;
    [SerializeField] private TMP_Text weaponLevel   ;

    // variables for the death and save features
    private float saveTime = 2f;
    private bool cryin = false;
    private float remainingSaveTime;
    public bool saveKeyPressed = false;
    private InputAction saveAction;
    private Prince princeToSave;

    // fire assist
    private bool assistOn = true;
    private InputAction assistAction;
    private Enemy target;
    [SerializeField] private float EnemyDetectionRange = 30f;

    // ammo filling
    private bool atAmmunition = false;
    private Ammunition supplyPoint;
    private bool filled = false;

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

        assistAction = map.FindAction("FireAssist");
        assistAction.started += ToggleAssist;
    }

    // Update is called once per frame
    void Update()
    {
        if(!freeze)
        {
            transform.Translate(input * speed * stats.GetMoveSpeedFactor() * Time.deltaTime);
            CheckFlipSpriteCondition();
            Fire();
            SaveTargetPrince();
            weaponStat.text = gunController.GetGunStat();
            SetWeaponLevelText();
        } 
    }

    private void SetWeaponLevelText()
    {
        int lv = gunController.GetGunLevel();
        weaponLevel.text = "Lv" + lv;
        switch (lv)
        { 
            case 1:
                weaponLevel.color = Color.white;
                break;
            case 2:
                weaponLevel.color = Color.yellow;
                break;
            case 3:
                weaponLevel.color = new Color(1.0f, 0.64f, 0.0f);
                break;
            case 4:
                weaponLevel.color = Color.red;
                break;
            default:
                weaponLevel.color = Color.white;
                break;
        }
    }

    private void CheckFlipSpriteCondition()
    {
        Transform character = transform.Find("Character");
        if(assistOn && target)
        {
            Transform targetPos = target.transform;
            bool cond1 = targetPos.position.x > transform.position.x && Mathf.Sign(character.localScale.x) < 0;
            bool cond2 = targetPos.position.x < transform.position.x && Mathf.Sign(character.localScale.x) > 0;

            if(cond1 || cond2)
            {
                FlipSprite();
            }
        }
        else
        {
            bool cond1 = input.x > 0 && Mathf.Sign(character.localScale.x) < 0;
            bool cond2 = input.x < 0 && Mathf.Sign(character.localScale.x) > 0;
            if (cond1 || cond2)
            {
                FlipSprite();
            }
        }
    }

    private void FlipSprite()
    {
        Transform character = transform.Find("Character");
        float x = character.localScale.x;
        float y = character.localScale.y;
        float z = character.localScale.z;
        Vector3 newScale = new Vector3(-x, y, z);
        character.localScale = newScale;
        gunController.Rotate();
    }

    private void ToggleAssist(InputAction.CallbackContext context)
    {
        assistOn = !assistOn;
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

    // When enter key is pressed, either fire or fill ammo depending on the positon of the prince
    public void Fire()
    {
        if(atAmmunition && !filled && supplyPoint && supplyPoint.ifActive() && !cryin)
        {
            if (firing)
            {
                //Debug.Log("tries to fill ammo");
                supplyPoint.FillAmmo(this);
            }
            else
            {
                supplyPoint.CancelRefill(this);
            }
        } else if(receiveInput && firing && !cryin)
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
        }
    }

    public void FillAmmo()
    {
        gunController.RefillAmmo();
        filled = true;
        atAmmunition = false;
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

    public Prince GetPrinceToSave()
    {
        return princeToSave;
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

    public bool GetFireAssistStatus()
    {
        return assistOn;
    }

    public void SetTarget(Enemy enemyTarget)
    {
        target = enemyTarget;
    }

    public float GetDetectionRange()
    {
        return EnemyDetectionRange;
    }

    public void EnterAmmoSupply(Ammunition ammu)
    {
        atAmmunition = true;
        supplyPoint = ammu;

    }

    public void ExitAmmoSupply()
    {
        atAmmunition = false;
        supplyPoint = null;
    }

    public void RefreshFillChance()
    {
        filled = false;
    }

    public bool GetFillStatus()
    {
        return filled;
    }
}
