using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlitBulletAward : Award
{
    protected override void GiveAwards(GameObject prince)
    {
        Prince tempPrince = prince.GetComponent<Prince>();
        //tempPrince.SetInfBullets(true);
    }
}
