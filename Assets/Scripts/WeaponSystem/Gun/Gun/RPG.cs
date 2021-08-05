using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Gun
{
    private int microRocketNum = 0;
    public override void LevelUp()
    {
        level++;
        maxAmmo += 2;
        dmg += 10;

        switch (level)
        {
            case 3:
                attackCd = 0.65f;
                microRocketNum = 4;
                bulletSpeed = 30;
                break;
            case 5:
                attackCd = 0.6f;
                microRocketNum = 8;
                break;
            case 7:
                attackCd = 0.5f;
                microRocketNum = 12;
                bulletSpeed = 35;
                break;
            default:
                break;
        }
    }
    public override void Fire()
    {
        if (lastShoot < attackCd * stats.GetShootSpeedFactor() * stats.GetAttackSpeedReduct())
        {
            return;
        }
        else if (bullet[stats.GetBulletId()] && ammo > 0)
        {
            if (!stats.GetPassiveSkillsStats("InfAmmo") && ammo > 0)
                ammo--;
            audioSource.PlayOneShot(shootSound);
            //AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
            FindObjectOfType<CamShakeController>().ShakeAtController(0.2f, shakeAmplitude, 5f);
            Bullet newBullet = Instantiate(bullet[stats.GetBulletId()], firePoint.transform.position, Quaternion.Euler(0, 0, angle));
            
            ((Rocket)newBullet).SetMicroRocketNum(microRocketNum);
            InitBullet(newBullet);

            lastShoot = 0f;
            if (shootEffects[stats.GetBulletId()])
            {
                GameObject obj = Instantiate(shootEffects[stats.GetBulletId()], firePoint.transform.position, Quaternion.Euler(0, 0, angle));
                Destroy(obj, 1f);
            }
        }
    }
}
