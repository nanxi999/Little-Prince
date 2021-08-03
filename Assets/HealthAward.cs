using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthAward : Award
{
    public float[] value;

    private float selected;

    protected override void Start()
    {
        base.Start();
        selected = value[Random.Range(0, value.Length)];
        TMP_Text descriptionText = description.Find("Description").GetComponent<TMP_Text>();
        descriptionText.SetText("Increase {0} health limit of the prince.", selected);
        description.gameObject.SetActive(false);
    }

    protected override void GiveAwards(GameObject prince)
    {
        Prince tempPrince = prince.GetComponent<Prince>();
        tempPrince.SetMaxHealth(tempPrince.GetMaxHealth() + selected);
        tempPrince.SetHealth(tempPrince.GetMaxHealth());
    }
}
