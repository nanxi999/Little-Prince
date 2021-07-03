using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armory : MonoBehaviour
{
    public GameObject[] guns;
    // Start is called before the first frame update
    public GameObject GetRandomGun()
    {
        int size = guns.Length;
        int index = Random.Range(0, size);
        return guns[index];
    }
 
    
}
