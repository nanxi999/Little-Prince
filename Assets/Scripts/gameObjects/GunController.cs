using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Prince prince;
    
    private Gun gun;
    private Armory armory;
    private GameObject gunObj;
    private int gunIndex = 0;
    private int gunCount = 1;
    private List<Gun> gunsArmory;
    private StatsManager stats;
    private Enemy target;
    private bool assistActive;
    private float detectRange;
    [SerializeField] protected float targetSwitchThreshold = 10f;
    // Start is called before the first frame update


    void Start()
    {
        armory = FindObjectOfType<Armory>();
        prince = transform.parent.parent.GetComponent<Prince>();
        stats = prince.GetComponent<StatsManager>();
        gunsArmory = new List<Gun>();
        gun = Instantiate(prince.initialGun, transform);
        gunsArmory.Add(gun);
        detectRange = prince.GetDetectionRange();
    }

    // Update is called once per frame
    void Update()
    {
        assistActive = prince.GetFireAssistStatus();
        SelectTarget();
        Rotate();
        prince.SetTarget(target);
        if (target)
        {
            target.SelectAsTarget(true);
        }
    }

    public Gun HasGun(Gun tempGun)
    {
        foreach(Gun g in gunsArmory)
        {
            if (g.GetName().Equals(tempGun.GetName())) {
                return g;
            }
        }
        return null;
    }

    private void SelectTarget()
    {
        Enemy nextTarget = null;
        float curDist = float.MaxValue;
        float minDist = float.MaxValue;
        if(target)
        {
            curDist = Vector2.Distance(gun.GetFirePoint().position, target.transform.position);
        }

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach(Enemy enemy in enemies)
        {
            if (enemy != target)
            {
                float distance = Vector2.Distance(enemy.transform.position, gun.GetFirePoint().position);
                if(distance < minDist)
                {
                    minDist = distance;
                    nextTarget = enemy;
                }
            }
        }

        if(curDist - minDist > targetSwitchThreshold && minDist < detectRange)
        {
            if(target)
            {
                target.SelectAsTarget(false);
            }
            target = nextTarget;
        }
    }

    public void AddGun(Gun gunPrefab)
    {
        Gun tempGun = HasGun(gunPrefab);
        if (tempGun)
        {
            tempGun.LevelUp();
            tempGun.SetAmmo((int)Mathf.Ceil(gun.GetMaxAmmo() * stats.GetAmmoFactor()));
            return;
        } else
        {
            gunCount++;
            gunIndex = gunCount - 1; 

            //Switch to the new gun
            gun.gameObject.SetActive(false);
            gun = Instantiate(gunPrefab, transform) as Gun;
            gun.SetMaxAmmo((int)Mathf.Ceil(gun.GetMaxAmmo() * stats.GetAmmoFactor()));

            //Add new gun to gunsArmory
            gunsArmory.Add(gun);
        }
    }

    public void RemoveGun()
    {
        gunIndex = 0;
        gunCount--;
        gunsArmory.Remove(gun);
        Destroy(gun.gameObject);

        //Switch to default gun
        gunObj = transform.GetChild(gunIndex).gameObject;
        gunObj.SetActive(true);
        gun = gunObj.GetComponent<Gun>();
    }

    public void SwitchNextGun()
    {
        gunIndex = (gunIndex + 1) % gunCount;
        gun.gameObject.SetActive(false);
        gunObj = transform.GetChild(gunIndex).gameObject;
        gunObj.SetActive(true);
        gun = gunObj.GetComponent<Gun>();
    }

    public void Rotate()
    {
        float angle = 0;
        if(target && assistActive)
        {
            angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg + 90f;
        } else
        {
            Vector2 dir = prince.GetMoveDir();
            if (dir == Vector2.zero) { return; }
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
        gun.SetAngle(angle);
    }

    public string GetGunStat()
    {
        if(!gun) { return "?"; }
        if(gun.GetName().Equals("Rifle"))
        { 
            return gun.GetName() + ": \u221E";
        } else
        {
            return gun.GetName() + ": " + gun.GetAmmo();
        }
    }

    public int GetGunLevel()
    {
        if (gun)
            return gun.GetLevel();
        else
            return -1;
    }

    public List<Gun> GetArmory()
    {
        return gunsArmory;
    }

    public void RefillAmmo()
    {
        gun.RefillAmmo();   
    }
}
