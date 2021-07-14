using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpeedAward : Award
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
        StatsManager stats = prince.GetComponent<StatsManager>();
        stats.SetShootSpeedFactor(stats.GetShootSpeedFactor() * (1 - selected));
    }
}
