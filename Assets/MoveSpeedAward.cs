using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveSpeedAward : Award
{
    public float[] percentage;

    private float selected;

    protected override void Start()
    {
        base.Start();
        selected = percentage[Random.Range(0, percentage.Length)];
        TMP_Text descriptionText = description.Find("Description").GetComponent<TMP_Text>();
        descriptionText.SetText("Increase the movement speed of the prince by {0}%. The movement speed can be increased by at most 50%.", selected * 100);
        description.gameObject.SetActive(false);
    }

    protected override void GiveAwards(GameObject prince)
    {
        Prince tempPrince = prince.GetComponent<Prince>();
        StatsManager stats = tempPrince.GetComponent<StatsManager>();
        stats.SetMoveSpeedFactor(stats.GetMoveSpeedFactor() * (1 + selected));
    }
}
