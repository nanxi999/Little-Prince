using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBulletAward : Award
{
    public float[] percentage;
    public Transform description;

    private float selected;

    protected override void GiveAwards(GameObject prince)
    {
        Prince tempPrince = prince.GetComponent<Prince>();
        StatsManager stats = prince.GetComponent<StatsManager>();
        stats.SetBulletId(1);
    }
}
