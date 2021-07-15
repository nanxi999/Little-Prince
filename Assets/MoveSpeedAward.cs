using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedAward : Award
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
        tempPrince.SetMaxHealth(tempPrince.GetMaxHealth() * (1 + selected));
        tempPrince.SetHealth(tempPrince.GetMaxHealth());
    }
}
