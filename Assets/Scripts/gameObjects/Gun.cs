using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] LightBullet lightBullet;
    [SerializeField] float attackCd = 1f;
    [SerializeField] GameObject firePoint;

    private int target = 0;
    private Prince prince;
    private float lastShoot;

    // Start is called before the first frame update
    void Start()
    {
        lastShoot = attackCd;
        prince = FindObjectOfType<Prince>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        lastShoot += Time.deltaTime;
        if(lastShoot < attackCd)
        {
            return;
        } else if (Input.GetAxis("Fire1") == 1 && lightBullet)
        {   
            // direction of bullet

            // rotation of bullet
            Vector2 dir = prince.GetMoveDir();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
            Instantiate(lightBullet, firePoint.transform.position, Quaternion.Euler(0, 0, angle));
            lastShoot = 0f;
        }

    }

  
}
