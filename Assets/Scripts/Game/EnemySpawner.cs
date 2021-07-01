using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 3f;
    [SerializeField] Mutant[] enemyPrefabArray;

    bool spawn = true;
    LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();   
    }

    // Start is called before the first frame update
    public IEnumerator StartSpawning()
    {
        spawn = true;
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            if (spawn)
                SpawnAttacker();
        }
    }

    private void SpawnAttacker()
    {
        var enemyIndex = Random.Range(0, enemyPrefabArray.Length); 
        Spawn(enemyPrefabArray[enemyIndex]);
    }

    private void Spawn(Mutant myEnemy)
    {
        Mutant newEnemy = Instantiate(myEnemy, transform.position, transform.rotation) as Mutant;
        newEnemy.transform.parent = transform;
        newEnemy.SetDmg(levelController.GetLevel() + 10);
        newEnemy.SetSpeed((float)(levelController.GetLevel() * 0.2 + 3));
    }

    public void StopSpawning()
    {
        spawn = false;
    }
}
