using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerBullet : Bullet
{
    public GameObject target;

    protected override void Fly()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        Vector3 dir = newPos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = newPos;
    }
}
