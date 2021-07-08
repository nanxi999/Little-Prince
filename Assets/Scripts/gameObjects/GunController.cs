﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Prince prince;
    
    private Gun gun;
    private Armory armory;
    private GameObject gunObj;
    private int gunIndex = 0;
    private int gunCount = 1;
    private List<string> gunsArmory;
    // Start is called before the first frame update


    void Start()
    {
        armory = FindObjectOfType<Armory>();
        prince = transform.parent.GetComponent<Prince>();
        gunsArmory = new List<string>();
        //gunObj = armory.GetRandomGun();
        //GameObject obj = Instantiate(gunObj, transform);
        //gun = obj.GetComponent<Gun>();
        //gun.SetPrince(prince);
        //obj.transform.parent = transform;
        gun = Instantiate(prince.initialGun, transform);
        gunsArmory.Add(prince.initialGun.name);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate();
    }

    public void AddGun(Gun gunPrefab)
    {
        if (gunsArmory.Contains(gunPrefab.name.Split('(')[0]))
        {
            return;
        } else
        {
            gunCount++;
            gunIndex = gunCount - 1;
            gun.gameObject.SetActive(false);
            gun = Instantiate(gunPrefab, transform) as Gun;
            Debug.Log(gunPrefab.name);
            gun.SetPrince(prince);
            gun.transform.parent = transform;
        }
    }

    public void SwitchNextGun()
    {
        gunIndex = (gunIndex + 1) % gunCount;
        gun.gameObject.SetActive(false);
        gunObj = transform.GetChild(gunIndex).gameObject;
        gunObj.SetActive(true);
        gun = gunObj.GetComponent<Gun>();
    }

    public void Rotate()
    {
        Vector2 dir = prince.GetMoveDir();
        if(dir == Vector2.zero) { return; }
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        gun.SetAngle(angle);
    }
}
