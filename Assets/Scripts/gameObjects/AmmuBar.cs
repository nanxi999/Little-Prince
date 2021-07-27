using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmuBar : MonoBehaviour
{
    [SerializeField] private Ammunition supply;

    // Update is called once per frame
    void Update()
    {
        float progress = supply.GetCurTime();
        SetColour(Color.white);
        float totalProgress = supply.GetMaxTime();
        float percentage = progress / (float)totalProgress;
        transform.localScale = new Vector3(percentage, transform.localScale.y, transform.localScale.z);
    }

    public void SetColour(Color color)
    {
        transform.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }
}
