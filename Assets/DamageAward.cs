using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAward : Award
{
    public int[] percentage;

    private float selected;

    protected override void Start()
    {
        base.Start();
        selected = percentage[Random.Range(0, percentage.Length)];
    }

    protected override void GiveAwards(GameObject prince)
    {
        Prince tempPrince = prince.GetComponent<Prince>();
        StatsManager stats = tempPrince.GetComponent<StatsManager>();
        stats.SetDamageFactor(stats.GetDamageFactor() * (1 + selected));
    }
}
