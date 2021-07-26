using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchWizardSpawner : EnemySpawner
{
    int count = 0;
    public override IEnumerator StartSpawning()
    {
        if(levelController.GetLevel() % 2 == 0)
        {
            while(count < levelController.GetLevel() / 2)
            {
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
                SpawnAttacker();
                count++;
            }
        }
    }
}
