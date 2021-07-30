using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HurtAward : Award
{
    public float[] percentage;

    private float selected;

    protected override void Start()
    {
        base.Start();
        selected = percentage[Random.Range(0, percentage.Length)];
        TMP_Text descriptionText = description.Find("Description").GetComponent<TMP_Text>();
        descriptionText.text = descriptionText.text + " " + selected * 100 + "%";
        description.gameObject.SetActive(false);
    }

    protected override void GiveAwards(GameObject prince)
    {
        Prince tempPrince = prince.GetComponent<Prince>();
        StatsManager stats = prince.GetComponent<StatsManager>();
        stats.SetHurtFactor(stats.GetHurtFactor() * (1 - selected));
    }
}
