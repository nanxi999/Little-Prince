using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    public override void LevelUp()
    {
        if (level < maxLevel)
            level++;
        else
            return;
        switch (level)
        {
            case 2:
                dmg = dmg * 1.1f;
                bulletSpeed = bulletSpeed * 1.2f;
                break;
            case 3:
                dmg = dmg * 1.1f;
                pushBackForce = pushBackForce * 1.2f;
                break;
            case 4:
                dmg = dmg * 1.1f;
                bulletSpeed = bulletSpeed * 1.1f;
                pushBackForce = pushBackForce * 1.1f;
                break;
            default:
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
