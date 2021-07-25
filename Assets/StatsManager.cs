using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Attributes
    [SerializeField] private float shootSpeedFactor = 1f;
    [SerializeField] private float damageFactor = 1;
    [SerializeField] private float hurtFactor = 1;
    [SerializeField] private float moveSpeedFactor = 1;
    [SerializeField] private float ammoFactor = 1;
    [SerializeField] private int bulletId = 0;

    // Upper limits
    [SerializeField] private float moveSpeedFactorLimit = 2;

    // Passive skills
    [SerializeField] private Dictionary<string, bool> passiveSkills;

    private void Start()
    {
        passiveSkills = new Dictionary<string, bool>();
        passiveSkills.Add("InfAmmo", false);
    }

    public float GetShootSpeedFactor()
    {
        return shootSpeedFactor;
    }

    public void SetShootSpeedFactor(float val)
    {
        shootSpeedFactor = val;
    }

    public float GetDamageFactor()
    {
        return damageFactor;
    }

    public void SetDamageFactor(float val)
    {
        damageFactor = val;
    }

    public float GetAmmoFactor()
    {
        return ammoFactor;
    }

    public void SetAmmoFactor(float val)
    {
        ammoFactor = val;
    }

    public float GetHurtFactor()
    {
        return hurtFactor;
    }

    public void SetHurtFactor(float val)
    {
        hurtFactor = val;
    }

    public float GetMoveSpeedFactor()
    {
        return moveSpeedFactor;
    }

    public void SetMoveSpeedFactor(float val)
    {
        if(val > moveSpeedFactorLimit)
        {
            val = moveSpeedFactorLimit;
        }
        moveSpeedFactor = val;
    }

    public void ResetPassiveSkills()
    {
        List<string> keys = new List<string>();
        foreach(string key in new List<string>(passiveSkills.Keys))
        {
            passiveSkills[key] = false;
        }
    }

    public void StartPassiveSkills(string skillName)
    {
        passiveSkills[skillName] = true;
    }

    public bool GetPassiveSkillsStats(string skillName)
    {
        return passiveSkills[skillName];
    }

    public int GetBulletId()
    {
        return bulletId;
    }

    public void SetBulletId(int val)
    {
        bulletId = val;
    }
}
