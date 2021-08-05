using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Gun
{
    // Start is called before the first frame update
    public float spread;
    public float resetSpreadTime;

    private float contShootTime;
    private float startModifyingSpread = 0f;
    private bool firing = false;

    // Update is called once per frame
    public override void Update()
    {
        lastShoot += Time.deltaTime;
        if(lastShoot > attackCd)
        {
            contShootTime -= 3 * Time.deltaTime;
            if(contShootTime <= 0)
            {
                contShootTime = 0;
            }
        }
    }

    public override void LevelUp()
    {
        level++;
        switch (level)
        {
            case 3:
                maxAmmo = 300;
                dmg = dmg * 1.2f;
                attackCd = 0.12f;
                startModifyingSpread = 1;
                spread = 10;
                break;
            case 5:
                dmg = dmg * 1.2f;
                attackCd = 0.1f;
                maxAmmo = 400;
                startModifyingSpread = 2;
                spread = 8;
                break;
            case 7:
                dmg = dmg * 1.2f;
                maxAmmo = 500;
                attackCd = 0.08f;
                startModifyingSpread = 3;
                spread = 6;
                break;
            default:
                dmg += 3;
                maxAmmo += 50;
                break;
        }
        return;
    }

    public override void Fire()
    {
        contShootTime +=  Time.deltaTime;
        if(contShootTime > 5)
        {
            contShootTime = 5;
        }
        if (lastShoot < attackCd * stats.GetShootSpeedFactor() * stats.GetAttackSpeedReduct())
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
            // modify spread
            float spreadModify = 0;
            if (contShootTime > startModifyingSpread)
            {
                spreadModify = Random.Range(-spread, spread);
                float amplifier = (contShootTime - startModifyingSpread) * 0.6f;
                if(amplifier > 1.5) { amplifier = 1.5f; }
                spreadModify = spreadModify * amplifier;
            }

            audioSource.PlayOneShot(shootSound);
            FindObjectOfType<CamShakeController>().ShakeAtController(0.2f, shakeAmplitude, 5f);
            Bullet newBullet = Instantiate(bullet[stats.GetBulletId()], firePoint.transform.position, Quaternion.Euler(0, 0, angle + spreadModify));
            InitBullet(newBullet);

            lastShoot = 0f;
            if (shootEffects[stats.GetBulletId()])
            {
                GameObject obj = Instantiate(shootEffects[stats.GetBulletId()], firePoint.transform.position, Quaternion.Euler(0, 0, angle));
                Destroy(obj, 2);
            }
        }
    }
}
