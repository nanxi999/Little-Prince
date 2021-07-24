using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootSpeedAward : Award
{
    public float[] percentage;
    public Transform description;

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
        StatsManager stats = prince.GetComponent<StatsManager>();
        stats.SetShootSpeedFactor(stats.GetShootSpeedFactor() * (1 - selected));
    }
}
