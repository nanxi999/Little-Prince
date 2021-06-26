using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Gun gun;
    private Prince prince;
    // Start is called before the first frame update
    void Start()
    {
        prince = FindObjectOfType<Prince>();
        gun = GetComponentInChildren<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    public void Rotate()
    {
        Vector2 dir = prince.GetMoveDir();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.Log(angle);
    }
}
