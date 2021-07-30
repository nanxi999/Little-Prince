using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBulletAward : Award
{
    private float selected;

    protected override void GiveAwards(GameObject prince)
    {
        Prince tempPrince = prince.GetComponent<Prince>();
        StatsManager stats = prince.GetComponent<StatsManager>();
        stats.SetBulletId(1);
    }
}
