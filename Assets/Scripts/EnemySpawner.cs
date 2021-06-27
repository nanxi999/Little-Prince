using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 3f;
    [SerializeField] Hurtable[] enemyPrefabArray;

    bool spawn = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnAttacker();
        }
    }

    private void SpawnAttacker()
    {
        var enemyIndex = Random.Range(0, enemyPrefabArray.Length); 
        Spawn(enemyPrefabArray[enemyIndex]);
    }

    private void Spawn(Hurtable myEnemy)
    {
        Hurtable newEnemy = Instantiate(myEnemy, transform.position, transform.rotation) as Hurtable;
        newEnemy.transform.parent = transform;
    }

    public void StopSpawning()
    {
        spawn = false;
    }
}
