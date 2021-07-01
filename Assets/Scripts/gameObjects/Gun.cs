using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] LightBullet lightBullet;
    [SerializeField] float attackCd = 1f;
    [SerializeField] GameObject firePoint;

    public GameObject shootEffect;
    private int target = 0;
    private Prince prince;
    private float lastShoot;
    private float angle;

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

    public void Fire()
    {
        if(lastShoot < attackCd)
        {
            return;
        } else if (lightBullet)
        {
            Instantiate(lightBullet, firePoint.transform.position, Quaternion.Euler(0, 0, angle));
            lastShoot = 0f;

            if(shootEffect)
            {
                Instantiate(shootEffect, firePoint.transform.position, Quaternion.Euler(0, 0, angle));
            }
        }

    }

    public void SetAngle(float newAngle)
    {
        angle = newAngle;
    }

  
}
