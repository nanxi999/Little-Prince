using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    public int spread;
    public int bulletNum;

    public override void Fire()
    {
        if (lastShoot < attackCd)
        {
            return;
        }
        else if (bullet)
        {
            ammo--;
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
            Bullet b = Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(0, 0, angle));
            b.SetDmg(dmg);
            b.SetShooter(prince.gameObject);          

            for (int i = 1; i <= bulletNum/2; i++)
            {
                b = Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(0, 0, angle + spread * i));
                b.SetDmg(dmg);
                b.SetShooter(prince.gameObject);
                b = Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(0, 0, angle - spread * i));
                b.SetDmg(dmg);
                b.SetShooter(prince.gameObject);
            }
            FindObjectOfType<CamShakeController>().ShakeAtController(0.2f, shakeAmplitude, 5f);
            lastShoot = 0f;

            if (shootEffect)
            {
                GameObject obj = Instantiate(shootEffect, firePoint.transform.position, Quaternion.Euler(0, 0, angle));
                Destroy(obj, 2);
            }
        }
        if (ammo <= 0)
        {
            transform.parent.GetComponent<GunController>().RemoveGun();
        }
    }
}
