using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaBullet : Bullet
{
    protected override void Fly()
    {
        Vector3 tempDir = new Vector3(1f, 0, 0);
        Debug.Log(transform.position);
        transform.Translate(tempDir * speed * Time.deltaTime);
    }
}
