using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchWizardSpawner : EnemySpawner
{
    public int firstSpawnLevel = 10;
    int count = 0;
    public override IEnumerator StartSpawning()
    {
        if(levelController.GetLevel() % firstSpawnLevel == 0)
        {
            while(count < levelController.GetLevel() / 10)
            {
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
                SpawnAttacker();
                count++;
            }
        }
    }
}
