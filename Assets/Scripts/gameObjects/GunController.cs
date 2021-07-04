using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Prince prince;
    private Gun gun;
    private Armory armory;
    private GameObject gunObj;
    // Start is called before the first frame update


    void Start()
    {
        Debug.Log("Set Up");
        armory = FindObjectOfType<Armory>();
        gunObj = armory.GetRandomGun();
        GameObject obj = Instantiate(gunObj, transform);
        gun = obj.GetComponent<Gun>();
        gun.SetPrince(prince);
        obj.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate();
    }

    public void SetPrince(Prince prince)
    {
        this.prince = prince;
    }

    public void Rotate()
    {
        Vector2 dir = prince.GetMoveDir();
        if(dir == Vector2.zero) { return; }
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        gun.SetAngle(angle);
    }
}
