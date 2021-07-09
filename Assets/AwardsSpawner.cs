using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsSpawner : MonoBehaviour
{
    public Award[] awardPrefabArray;

    private void Start()
    {
        SpawnAwards();
    }

    public void SpawnAwards()
    {
        Award award1 = Instantiate(GetRandomAward(), transform.position, transform.rotation);
        Award award2 = Instantiate(GetRandomAward(), transform.position, transform.rotation);
        Award award3 = Instantiate(GetRandomAward(), transform.position, transform.rotation);

        award1.GetComponent<Rigidbody2D>().velocity = new Vector3 (-6f, 5f, 0f);
        award2.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 5f, 0f);
        award3.GetComponent<Rigidbody2D>().velocity = new Vector3(6f, 5f, 0f);
    }

    private Award GetRandomAward()
    {
        return awardPrefabArray[Random.Range(0, awardPrefabArray.Length)];
    }
}
