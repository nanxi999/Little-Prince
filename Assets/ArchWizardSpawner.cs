using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchWizardSpawner : EnemySpawner
{
    public int firstSpawnLevel = 10;
    private int count = 0;
    public override IEnumerator StartSpawning()
    {
        count = 0;
        if(levelController.GetLevel() % firstSpawnLevel == 0)
        {
            while(count < levelController.GetLevel() / firstSpawnLevel)
            {
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
                SpawnAttacker();
                count++;
            }
        }
    }
}
