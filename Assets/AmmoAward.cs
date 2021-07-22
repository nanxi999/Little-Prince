using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoAward : Award
{
    public float[] percentage;

    private float selected;

    protected override void Start()
    {
        base.Start();
        selected = percentage[Random.Range(0, percentage.Length)];
    }

    protected override void GiveAwards(GameObject prince)
    {
        Prince tempPrince = prince.GetComponent<Prince>();
        StatsManager stats = prince.GetComponent<StatsManager>();
        stats.SetAmmoFactor(stats.GetAmmoFactor() * (1 + selected));
    }
}
