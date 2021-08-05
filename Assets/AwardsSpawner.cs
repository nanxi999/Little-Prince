using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsSpawner : MonoBehaviour
{
    public Award[] normalAwardPrefabArray;
    public Award[] gunAwardPrefabArray;
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
        int playerCount = levelController.GetPlayerNumber();
        if(playerCount == 1)
        {
            SpawnNumberOfAwards(3);
        } else if(playerCount == 2)
        {
            SpawnNumberOfAwards(4);
        } else if(playerCount == 3)
        {
            SpawnNumberOfAwards(5);
        } else if(playerCount >= 4)
        {
            SpawnNumberOfAwards(6);
        }
    }

    /*
    public void SpawnAwards()
    {
        Vector3 offset = new Vector3(0, -1, 0);
        GameObject effect = Instantiate(spawnEffect, transform.position, transform.rotation);
        Award award1, award2, award3, award4;
        List<Award> normalAwards, gunAwards;

        normalAwards = FilterAwards(normalAwardPrefabArray);
        gunAwards = FilterAwards(gunAwardPrefabArray);

        award1 = Instantiate(GetRandomAward(normalAwards), transform.position + offset, transform.rotation);
        award2 = Instantiate(GetRandomAward(normalAwards), transform.position + offset, transform.rotation);
        award3 = Instantiate(GetRandomAward(normalAwards), transform.position + offset, transform.rotation);
        award4 = Instantiate(GetRandomAward(gunAwards), transform.position + offset, transform.rotation);

        if (award1 && award2 && award3)
        {
            award1.GetComponent<Rigidbody2D>().velocity = new Vector3(-9f, 5f, 0f);
            award2.GetComponent<Rigidbody2D>().velocity = new Vector3(-3f, 5f, 0f);
            award3.GetComponent<Rigidbody2D>().velocity = new Vector3(3f, 5f, 0f);
            award4.GetComponent<Rigidbody2D>().velocity = new Vector3(9f, 5f, 0f);
        } else
        {
            Debug.Log("Fail to get ramdom award");
        }
        

        Destroy(effect, 2);
    }*/

    public void SpawnNumberOfAwards(int num)
    {
        if(num < 3)
        {
            Debug.Log("Cannot spawn less than 3 awards");
        } else if (num > 6)
        {
            Debug.Log("Cannot spawn more than 6 awards");
        }
        Vector3 offset = new Vector3(0, -1, 0);
        float[] gaps = { 12f, 18f, 20f, 24f };
        float gap = gaps[num - 3] / (num - 1);
        float left = gaps[num - 3] / 2;
        int gunAwardNum = (num - 1) / 2;
        int count = 0;
        Award award;
        List<Award> normalAwards, gunAwards;

        normalAwards = FilterAwards(normalAwardPrefabArray);
        gunAwards = FilterAwards(gunAwardPrefabArray);

        for (int i = 0; i < num; i++)
        {
            if(count < gunAwardNum)
            {
                award = Instantiate(GetRandomAward(gunAwards), transform.position + offset, transform.rotation);
                count++;
            }
            else
                award = Instantiate(GetRandomAward(normalAwards), transform.position + offset, transform.rotation);
            award.GetComponent<Rigidbody2D>().velocity = new Vector3((-left) + i * gap, 5f, 0f);
        }
    }

    private List<Award> FilterAwards(Award[] awardPrefabArray)
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
