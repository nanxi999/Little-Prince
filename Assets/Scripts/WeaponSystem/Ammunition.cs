using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject textDisplay;
    [SerializeField] private GameObject progressObj;
    [SerializeField] bool active = false;
    [SerializeField] float refillTime = 1.5f;
    [SerializeField] GameObject Lights;
    Prince curPrince;

    private float curTime = 0f;

    void Start()
    {   
        if(progressObj)
            Debug.Log("progressObj is not null");
    }

    // Update is called once per frame
    void Update()
    {
        Lights.SetActive(active);
    }

    public bool ifActive()
    {
        return active;
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
            progressObj.SetActive(true);
            //transform.Find("Progress").gameObject.SetActive(true);
            active = true;
            Prince[] princes = FindObjectsOfType<Prince>();
            foreach(Prince prince in princes)
            {
                prince.RefreshFillChance();
            }
            curTime = 0f;
        } else
        {
            textDisplay.SetActive(false);
            progressObj.SetActive(false);
            active = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Prince prince = collision.gameObject.GetComponent<Prince>();
        if (prince && !prince.GetFillStatus())
        {
            prince.EnterAmmoSupply(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("ee");
        Prince prince = collision.gameObject.GetComponent<Prince>();
        if (prince)
        {
            prince.ExitAmmoSupply();
            if (prince == curPrince)
            {
                CancelRefill(prince);
            }
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
