using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    public int spread;
    public int bulletNum;

    public override void LevelUp()
    {
        if (level < maxLevel)
            level++;
        else
            return;
        switch (level)
        {
            case 2:
                bulletNum = 5;
                maxAmmo = 60;
                attackCd = attackCd * 0.8f;
                break;
            case 3:
                attackCd = attackCd * 0.6f;
                maxAmmo = 80;
                break;
            case 4:
                dmg = dmg * 0.6f;
                attackCd = 0.15f;
                maxAmmo = 130;
                break;
            default:
                break;
        }
    }

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
            audioSource.PlayOneShot(shootSound);
            Bullet b = Instantiate(bullet[stats.GetBulletId()], firePoint.transform.position, Quaternion.Euler(0, 0, angle));
            InitBullet(b);
                     

            for (int i = 1; i <= bulletNum/2; i++)
            {
                b = Instantiate(bullet[stats.GetBulletId()], firePoint.transform.position, Quaternion.Euler(0, 0, angle + spread * i));
                InitBullet(b);

                b = Instantiate(bullet[stats.GetBulletId()], firePoint.transform.position, Quaternion.Euler(0, 0, angle - spread * i));
                InitBullet(b);
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
