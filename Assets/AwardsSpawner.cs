using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsSpawner : MonoBehaviour
{
    public Award[] awardPrefabArray;
    public GameObject spawnEffect;

    public void StartSpawning()
    {
        GetComponent<Animator>().SetTrigger("Spawn");
    }

    public void SpawnAwards()
    {
        Vector3 offset = new Vector3(0, -1, 0);
        GameObject effect = Instantiate(spawnEffect, transform.position, transform.rotation);
        Award award1 = Instantiate(GetRandomAward(), transform.position + offset, transform.rotation);
        Award award2 = Instantiate(GetRandomAward(), transform.position + offset, transform.rotation);
        Award award3 = Instantiate(GetRandomAward(), transform.position + offset, transform.rotation);

        award1.GetComponent<Rigidbody2D>().velocity = new Vector3 (-6f, 5f, 0f);
        award2.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 5f, 0f);
        award3.GetComponent<Rigidbody2D>().velocity = new Vector3(6f, 5f, 0f);

        Destroy(effect, 2);
    }

    private Award GetRandomAward()
    {
        return awardPrefabArray[Random.Range(0, awardPrefabArray.Length)];
    }
}
