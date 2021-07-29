using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    public int spread;
    public int bulletNum;

    public override void Fire()
    {
        if (lastShoot < attackCd * stats.GetShootSpeedFactor())
        {
            return;
        }
        else if (bullet[stats.GetBulletId()] && ammo > 0)
        {
            if(!stats.GetPassiveSkillsStats("InfAmmo"))
                ammo--;
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
            Bullet b = Instantiate(bullet[stats.GetBulletId()], firePoint.transform.position, Quaternion.Euler(0, 0, angle));
            b.SetDmg(dmg);
            b.SetShooter(prince.gameObject);          

            for (int i = 1; i <= bulletNum/2; i++)
            {
                b = Instantiate(bullet[stats.GetBulletId()], firePoint.transform.position, Quaternion.Euler(0, 0, angle + spread * i));
                b.SetDmg(dmg);
                b.SetShooter(prince.gameObject);
                b = Instantiate(bullet[stats.GetBulletId()], firePoint.transform.position, Quaternion.Euler(0, 0, angle - spread * i));
                b.SetDmg(dmg);
                b.SetShooter(prince.gameObject);
            }
            FindObjectOfType<CamShakeController>().ShakeAtController(0.2f, shakeAmplitude, 5f);
            lastShoot = 0f;

            if (shootEffects[stats.GetBulletId()])
            {
                GameObject obj = Instantiate(shootEffects[stats.GetBulletId()], firePoint.transform.position, Quaternion.Euler(0, 0, angle));
                Destroy(obj, 2);
            }
        }
    }
}
