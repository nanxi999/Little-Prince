using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsSpawner : MonoBehaviour
{
    public Award[] awardPrefabArray;
    public GameObject spawnEffect;

    private LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    public void StartSpawning()
    {
        GetComponent<Animator>().SetTrigger("Spawn");
    }

    public void SpawnAwards()
    {
        Vector3 offset = new Vector3(0, -1, 0);
        GameObject effect = Instantiate(spawnEffect, transform.position, transform.rotation);
        Award award1, award2, award3;
        List<Award> awards;
        if(FindObjectOfType<LevelController>().GetLevel() % 4 == 0)
        {
            awards = GetValidGunAwards();
        } else
        {
            awards = GetValidAwards();
        }
        award1 = Instantiate(GetRandomAward(awards), transform.position + offset, transform.rotation);
        award2 = Instantiate(GetRandomAward(awards), transform.position + offset, transform.rotation);
        award3 = Instantiate(GetRandomAward(awards), transform.position + offset, transform.rotation);

        if (award1 && award2 && award3)
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

    private List<Award> GetValidAwards()
    {
        List<Award> validAwards = new List<Award>();
        foreach(Award award in awardPrefabArray)
        {
            if(levelController.GetLevel() >= award.GetFirstSpawnLevel())
            {
                validAwards.Add(award);
            }
        }
        return validAwards;
    }

    private List<Award> GetValidGunAwards()
    {
        List<Award> validAwards = new List<Award>();
        foreach (Award award in awardPrefabArray)
        {
            if (levelController.GetLevel() >= award.GetFirstSpawnLevel() && award.GetComponent<GunAward>())
            {
                validAwards.Add(award);
            }
        }
        return validAwards;
    }

    private Award GetRandomAward(List<Award> awards)
    {
        float weightSum = 0;
        float randomVal = Random.value;
        float s = 0;

        for (int i = 0; i < awards.Count; i++)
        {
            weightSum += awards[i].GetWeight();
        }
        
        for (int i = 0; i < awards.Count; i++)
        {
            if (awards[i].GetWeight() <= 0)
                continue;
            s += awards[i].GetWeight() / weightSum;
            if (s >= randomVal)
                return awards[i];
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
