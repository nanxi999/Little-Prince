﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private float shootSpeedFactor = 1f;
    [SerializeField] private float damageFactor = 1;
    [SerializeField] private float hurtFactor = 1;
    [SerializeField] private float moveSpeedFactor = 1;
    [SerializeField] private float ammoFactor = 1;

    [SerializeField] private Dictionary<string, int> passiveSkills;

    [SerializeField] private float moveSpeedFactorLimit = 2;

    private void Start()
    {
        passiveSkills = new Dictionary<string, int>();
        passiveSkills.Add("infBullets", 0);
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

    public void StartInfBullets(int levels)
    {
        passiveSkills["infBullets"] = levels;
    }

    public void ReducePassiveSkillsLevels()
    {
        return;
    }
}
