using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAward : Award
{
    public Gun gunPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void GiveAwards() 
    {
        if(gunPrefab)
        {
            FindObjectOfType<GunController>().AddGun(gunPrefab);
        } else
        {
            Debug.Log("Gun prefab not set");
        }
        
    }
}
