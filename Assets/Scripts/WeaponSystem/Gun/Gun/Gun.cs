using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] protected string gunName;
    [SerializeField] protected int maxAmmo;
    
    public float dmg;
    public float attackCd = 1f;
    public float bulletLifetime;
    public float bulletSpeed;
    public float pushBackForce;

    [Header("Level Settings")]
    public int level;
    public int maxLevel;

    [Header("Other Settings")]
    public Bullet[] bullet;     // 0:normal  1:ice 
    public GameObject firePoint;
    public AudioClip shootSound;
    public float shakeAmplitude;
    public GameObject[] shootEffects;

    protected int ammo;
    protected int target = 0;
    protected Prince prince;
    protected float lastShoot;
    protected float angle;
    protected StatsManager stats;


    // Start is called before the first frame update
    public virtual void Start()
    {
        level = 1;
        ammo = maxAmmo;
        if (!prince)
        {
            prince = GetComponentInParent<Prince>();
        }
        stats = GetComponentInParent<StatsManager>();
        lastShoot = attackCd;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        lastShoot += Time.deltaTime;
    }

    public void SetPrince(Prince prince)
    {
        this.prince = prince;
    }

    public virtual void Fire()
    {
        if (lastShoot < attackCd * stats.GetShootSpeedFactor())
        {
            return;
        }
        else if (bullet[stats.GetBulletId()])
        {
            if (!stats.GetPassiveSkillsStats("InfAmmo") && ammo > 0)
                ammo--;
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
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

    public void SetAngle(float newAngle)
    {
        angle = newAngle;
    }

    public string GetName()
    {
        return gunName;
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public void SetAmmo(int val)
    {
        ammo = val;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

    public void SetMaxAmmo(int val)
    {
        maxAmmo = val;
        ammo = maxAmmo;
    }

    public Transform GetFirePoint()
    {
        return firePoint.transform;
    }

    public void RefillAmmo()
    {
        ammo = maxAmmo;
    }

    public virtual void LevelUp()
    {
        return;
    }

    protected void InitBullet(Bullet bullet)
    {
        bullet.SetShooter(prince.gameObject);
        bullet.SetLifeTime(bulletLifetime);
        bullet.SetSpeed(bulletSpeed);
        bullet.SetDmg(dmg);
        bullet.SetPushBackForce(pushBackForce);
    }
}
