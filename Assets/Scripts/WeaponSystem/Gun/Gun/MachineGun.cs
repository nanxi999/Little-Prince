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
        if (level < maxLevel)
            level++;
        else
            return;
        switch (level)
        {
            case 2:
                attackCd = attackCd * 0.7f;
                maxAmmo = (int) (maxAmmo * 1.2f);
                break;
            case 3:
                dmg = dmg * 1.5f;
                break;
            case 4:
                attackCd = attackCd * 0.6f;
                maxAmmo = (int)(maxAmmo * 1.8f);
                break;
            default:
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
        if (lastShoot < attackCd * stats.GetShootSpeedFactor())
        {
            return;
        } 
        else if (bullet[stats.GetBulletId()] && ammo > 0)
        {
            if (!stats.GetPassiveSkillsStats("InfAmmo"))
                ammo--;

            // modify spread
            float spreadModify = 0;
            if (contShootTime > startModifyingSpread)
            {
                spreadModify = Random.Range(-spread, spread);
                float amplifier = (contShootTime - startModifyingSpread) * 0.6f;
                if(amplifier > 1.5) { amplifier = 1.5f; }
                spreadModify = spreadModify * amplifier;
            }

            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
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
