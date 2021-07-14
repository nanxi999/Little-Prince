using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthBar : MonoBehaviour
{
    private Hurtable hurtableObj;
    // Start is called before the first frame update
    void Start()
    {
        hurtableObj = GetComponentInParent<Hurtable>();
    }

    // Update is called once per frame
    void Update()
    {
        float health = hurtableObj.GetHealth();
        if (health > 0)
        {
            float percentage = health / (float)hurtableObj.GetMaxHealth();
            transform.localScale = new Vector3(percentage, transform.localScale.y, transform.localScale.z);
            
            // under 30 percent health, health bar flashes
            if(percentage < 0.3)
            {
                SetColour(Color.white);
            }
        }
    }

    public void SetColour(Color color)
    {
        transform.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }
}
