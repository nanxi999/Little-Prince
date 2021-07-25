using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthBar : MonoBehaviour
{
    private Prince prince;
    // Start is called before the first frame update
    void Start()
    {
        prince = GetComponentInParent<Prince>();
    }

    // Update is called once per frame
    void Update()
    {
        float health = prince.GetHealth();
        if (!prince.IsCryin())
        {
            float percentage = health / (float)prince.GetMaxHealth();
            transform.localScale = new Vector3(percentage, transform.localScale.y, transform.localScale.z);
            
            // under 30 percent health, health bar flashes
            if(percentage < 0.3)
            {
                SetColour(Color.white);
            } else
            {
                SetColour(Color.red);
            }
        } else
        {
            float requiredTime = prince.GetSaveTime();
            float remaininTime = prince.GetRemainSaveTime();
            float percentage = (requiredTime - remaininTime) / requiredTime;
            transform.localScale = new Vector3(percentage, transform.localScale.y, transform.localScale.z);
        }
    }

    public void SetColour(Color color)
    {
        transform.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }
}
