using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Gun
{
    private int microRocketNum = 0;
    public override void LevelUp()
    {
        if(level < maxLevel)
        {
            level++;
            maxAmmo += 1;
            switch (level)
            {
                case 3:
                    microRocketNum = 4;
                    bulletSpeed = 30;
                    break;
                case 5:
                    microRocketNum = 8;
                    break;
                case 7:
                    microRocketNum = 12;
                    bulletSpeed = 35;
                    break;
                default:
                    break;
            }
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
