using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    public override void LevelUp()
    {
        level++;
        switch (level)
        {
            case 3:
                dmg = 30;
                bulletSpeed = bulletSpeed * 1.5f;
                break;
            case 5:
                dmg = 40;
                pushBackForce = pushBackForce * 1.5f;
                break;
            case 7:
                dmg = 50;
                bulletSpeed = bulletSpeed * 1.5f;
                pushBackForce = pushBackForce * 1.5f;
                break;
            default:
                dmg += 5;
                break;
        }
        return;
    }

    public override void Fire()
    {
        if (lastShoot < attackCd * stats.GetShootSpeedFactor())
        {
            return;
        }
        else if (bullet[stats.GetBulletId()])
        {
            audioSource.PlayOneShot(shootSound);
            FindObjectOfType<CamShakeController>().ShakeAtController(0.2f, shakeAmplitude, 5f);
            Bullet newBullet = Instantiate(bullet[stats.GetBulletId()], firePoint.transform.position, Quaternion.Euler(0, 0, angle));
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
