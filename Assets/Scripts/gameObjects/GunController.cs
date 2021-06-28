﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Prince prince;
    private Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        prince = GetComponentInParent<Prince>();
        gun = GetComponentInChildren<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate();
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
