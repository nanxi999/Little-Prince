using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bullet;
    public float attackCd = 1f;
    public GameObject firePoint;
    public AudioClip shootSound;
    public float shakeAmplitude;
    public GameObject shootEffect;

    protected int target = 0;
    protected Prince prince;
    protected float lastShoot;
    protected float angle;

    // Start is called before the first frame update
    public virtual void Start()
    {
        lastShoot = attackCd;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        lastShoot += Time.deltaTime;
        if(!prince)
        {
            GetComponentInParent<Prince>();
        }
    }

    public void SetPrince(Prince prince)
    {
        this.prince = prince;
    }

    public virtual void Fire()
    {
        if(lastShoot < attackCd)
        {
            return;
        } else if (bullet)
        {
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
            FindObjectOfType<CamShakeController>().ShakeAtController(0.2f, shakeAmplitude, 5f);
            Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(0, 0, angle));
            lastShoot = 0f;

            if(shootEffect)
            {
                Instantiate(shootEffect, firePoint.transform.position, Quaternion.Euler(0, 0, angle));
            }
        }
    }

    public void SetAngle(float newAngle)
    {
        angle = newAngle;
    }

  
}
