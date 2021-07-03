using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    // Start is called before the first frame update
    void Start()
    {
        lastShoot = attackCd;
        prince = FindObjectOfType<Prince>();
    }

    // Update is called once per frame
    void Update()
    {
        lastShoot += Time.deltaTime;
    }

}
