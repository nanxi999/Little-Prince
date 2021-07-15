using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerBullet : Bullet
{
    public Transform target;

    public void SetTarget(Transform tempTarget)
    {
        target = tempTarget;
    }

    protected override void Fly()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
