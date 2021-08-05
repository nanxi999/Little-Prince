using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    public int spread;
    public int bulletNum;
    public float coolDownCd = 2f;
    public int shotsBeforeCoolDown;

    private float tempCoolDownCd = 0;
    private int shootCount = 0;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        tempCoolDownCd -= Time.deltaTime;
    }

    public override void LevelUp()
    {
        level++;
        switch (level)
        {
            case 3:
                shotsBeforeCoolDown = 6;
                maxAmmo = 60;
                coolDownCd = 0.5f;
                break;
            case 5:
                dmg = 35;
                bulletNum = 5;
                shotsBeforeCoolDown = 8;
                attackCd = 0.22f;
                maxAmmo = 80;
                break;
            case 7:
                dmg = dmg * 0.6f;
                attackCd = 0.15f;
                maxAmmo = 100;
                coolDownCd = 0f;
                break;
            default:
                dmg += 5;
                maxAmmo += 10;
                break;
        }
    }

    public override void Fire()
    {
        if (lastShoot < attackCd * stats.GetShootSpeedFactor() * stats.GetAttackSpeedReduct() || tempCoolDownCd > 0)
        {
            return;
        }
        else if (bullet[stats.GetBulletId()])
        {
            if (!stats.GetPassiveSkillsStats("InfAmmo"))
            {
                if (ammo <= 0)
                    return;
                ammo--;
            }

            shootCount++;
            if (shootCount >= shotsBeforeCoolDown)
            {
                tempCoolDownCd = coolDownCd;
                shootCount = 0;
            }         

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
