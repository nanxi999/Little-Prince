using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private float shootSpeedFactor = 1f;
    [SerializeField] private float damageFactor = 1;
    [SerializeField] private float hurtFactor = 1;
    [SerializeField] private float moveSpeedFactor = 1;
    [SerializeField] private float ammoFactor = 1;

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
}
