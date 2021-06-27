using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    int numberOfAttackers = 0;
    bool levelTimeFinished = false; 

    public void EnemySpawned()
    {
        numberOfAttackers++;
    }

    public void EnemyKilled()
    {
        numberOfAttackers--;
        if (numberOfAttackers <= 0 && levelTimeFinished)
        {
            Debug.Log("End Level");
        }
    }

    public void LevelTimerFinished()
    {
        levelTimeFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        EnemySpawner[] spawnerArray = FindObjectsOfType<EnemySpawner>();
        foreach (EnemySpawner spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }
}
