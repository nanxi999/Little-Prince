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

        if(award1 && award2 && award3)
        {
            award1.GetComponent<Rigidbody2D>().velocity = new Vector3(-6f, 5f, 0f);
            award2.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 5f, 0f);
            award3.GetComponent<Rigidbody2D>().velocity = new Vector3(6f, 5f, 0f);
        } else
        {
            Debug.Log("Fail to get ramdom award");
        }
        

        Destroy(effect, 2);
    }

    private Award GetRandomAward()
    {
        float weightSum = 0;
        float randomVal = Random.value;
        float s = 0;

        for (int i = 0; i < awardPrefabArray.Length; i++)
        {
            weightSum += awardPrefabArray[i].GetWeight();
        }
        
        for (int i = 0; i < awardPrefabArray.Length; i++)
        {
            if (awardPrefabArray[i].GetWeight() <= 0)
                continue;
            s += awardPrefabArray[i].GetWeight() / weightSum;
            if (s >= randomVal)
                return awardPrefabArray[i];
        }
        return null;
    }

    /*
    private int GetRandomAwardIdx()
    {
        float weightSum = 0;
        float randomVal = Random.value;
        float s = 0;

        for (int i = 0; i < awardPrefabArray.Length; i++)
        {
            weightSum += awardPrefabArray[i].GetWeight();
        }

        for (int i = 0; i < awardPrefabArray.Length; i++)
        {
            if (awardPrefabArray[i].GetWeight() <= 0)
                continue;
            s += awardPrefabArray[i].GetWeight() / weightSum;
            if (s >= randomVal)
                return i;
        }
        return -1;
    }

    private void TestRandom()
    {
        int[] count = new int[11];
        for(int i = 0; i < 1000; i++)
        {
            int idx = GetRandomAwardIdx();
            count[idx] += 1;
        }
        for (int i = 0; i < 11; i++)
        {
            Debug.Log(i + ":" + count[i]/1000f);
        }
    }*/
}
