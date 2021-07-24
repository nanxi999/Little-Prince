using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoAward : Award
{
    public float[] percentage;
    public Transform description;

    private float selected;

    protected override void Start()
    {
        base.Start();
        selected = percentage[Random.Range(0, percentage.Length)];
        TMP_Text descriptionText = description.Find("Description").GetComponent<TMP_Text>();
        descriptionText.SetText("Imcrease the ammo limit of all weapons by {0}%, and reload all your weapons.", selected * 100);
        description.gameObject.SetActive(false);
    }

    protected override void GiveAwards(GameObject prince)
    {
        Prince tempPrince = prince.GetComponent<Prince>();
        StatsManager stats = prince.GetComponent<StatsManager>();
        stats.SetAmmoFactor(stats.GetAmmoFactor() * (1 + selected));
    }
}
