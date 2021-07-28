using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject textDisplay;
    [SerializeField] bool active = false;
    [SerializeField] float refillTime = 1.5f;
    Prince curPrince;

    private float curTime = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FillAmmo(Prince prince)
    {
        if(!curPrince)
        {
            curPrince = prince;
        }

        if(prince == curPrince)
        {
            curTime += Time.deltaTime;
            if (curTime > refillTime)
            {
                curTime = 0;
                curPrince.FillAmmo();
            }
        }
    }

    public void CancelRefill(Prince prince)
    {
        if(prince == curPrince)
        {
            curPrince = null;
            curTime = 0f;
        }
    }
    public void SetAmmuActive(bool status)
    {
        if(status)
        {
            textDisplay.SetActive(true);
            active = true;
            Prince[] princes = FindObjectsOfType<Prince>();
            foreach(Prince prince in princes)
            {
                prince.RefreshFillChance();
            }
        } else
        {
            textDisplay.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            Debug.Log("entered");
            Prince prince = collision.gameObject.GetComponent<Prince>();
            if (prince && !prince.GetFillStatus())
            {
                prince.EnterAmmoSupply(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Prince prince = other.gameObject.GetComponent<Prince>();
        if(prince)
        {
            prince.ExitAmmoSupply();
        }
    }

    public float GetMaxTime()
    {
        return refillTime;
    }

    public float GetCurTime()
    {
        return curTime;
    }

}
