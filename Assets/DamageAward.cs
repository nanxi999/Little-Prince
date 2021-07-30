using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageAward : Award
{
    public float[] percentage;

    private float selected;

    protected override void Start()
    {
        base.Start();
        selected = percentage[Random.Range(0, percentage.Length)];
        TMP_Text descriptionText = description.Find("Description").GetComponent<TMP_Text>();
        descriptionText.SetText("Improve the damage of your weapons by {0}%", selected * 100);
        description.gameObject.SetActive(false);
    }

    protected override void GiveAwards(GameObject prince)
    {
        Prince tempPrince = prince.GetComponent<Prince>();
        StatsManager stats = tempPrince.GetComponent<StatsManager>();
        stats.SetDamageFactor(stats.GetDamageFactor() * (1 + selected));
    }
}
