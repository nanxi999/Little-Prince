using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [Header("Factors")]
    [SerializeField] private float shootSpeedFactor = 1f;
    [SerializeField] private float damageFactor = 1;
    [SerializeField] private float hurtFactor = 1;
    [SerializeField] private float moveSpeedFactor = 1;
    [SerializeField] private float ammoFactor = 1;
    [SerializeField] private int bulletId = 0;

    [Header("Limits")]
    [SerializeField] private float moveSpeedFactorLimit = 2.5f;
    [SerializeField] private float hurtFactorLimit = 0.7f;
    [SerializeField] private float shootSpeedFactorLimit = 0.5f;

    // Passive skills
    [SerializeField] private Dictionary<string, bool> passiveSkills;

    private float AttackSpeedReductFactor;
    private float MoveSpeedReductFactor;

    private void Start()
    {
        passiveSkills = new Dictionary<string, bool>();
        passiveSkills.Add("InfAmmo", false);
    }

    public void ClearFactors()
    {
        shootSpeedFactor = 1;
        damageFactor = 1;
        hurtFactor = 1;
        moveSpeedFactor = 1;
        ammoFactor = 1;
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
        hurtFactor = Mathf.Max(val, hurtFactorLimit);
    }

    public float GetMoveSpeedFactor()
    {
        return moveSpeedFactor;
    }

    public float GetMoveSpeedFactorLimit()
    {
        return moveSpeedFactorLimit;
    }

    public void SetMoveSpeedFactor(float val)
    {
        moveSpeedFactor = Mathf.Min(val, moveSpeedFactorLimit);
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
