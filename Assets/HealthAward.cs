using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthAward : Award
{
    public float[] percentage;
    public Transform description;

    private float selected;

    protected override void Start()
    {
        base.Start();
        selected = percentage[Random.Range(0, percentage.Length)];
        TMP_Text descriptionText = description.Find("Description").GetComponent<TMP_Text>();
        descriptionText.SetText("Increase the health limit of the prince by {0}% and heal the prince.", selected * 100);
        description.gameObject.SetActive(false);
    }

    protected override void GiveAwards(GameObject prince)
    {
        Prince tempPrince = prince.GetComponent<Prince>();
        tempPrince.SetMaxHealth(tempPrince.GetMaxHealth() * (1 + selected));
        tempPrince.SetHealth(tempPrince.GetMaxHealth());
    }
}
