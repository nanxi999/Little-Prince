using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlitBulletAward : Award
{
    protected override void GiveAwards(GameObject prince)
    {
        Prince tempPrince = prince.GetComponent<Prince>();
        StatsManager stats = tempPrince.GetComponent<StatsManager>();
        stats.StartPassiveSkills("InfAmmo");
    }
}
