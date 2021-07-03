using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    public int spread;
    public int bulletNum;
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

    public override void Fire()
    {
        if (lastShoot < attackCd)
        {
            return;
        }
        else if (bullet)
        {
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
            FindObjectOfType<CamShakeController>().ShakeAtController(0.2f, shakeAmplitude, 5f);
            Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(0, 0, angle));
            Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(0, 0, angle+15));
            Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(0, 0, angle + 30));
            Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(0, 0, angle - 30));
            Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(0, 0, angle-15));
            lastShoot = 0f;

            if (shootEffect)
            {
                Instantiate(shootEffect, firePoint.transform.position, Quaternion.Euler(0, 0, angle));
            }
        }
    }
}
