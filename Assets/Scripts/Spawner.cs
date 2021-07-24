using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 3f;
    [SerializeField] Enemy enemyPref;

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
        Spawn(enemyPref);
    }

    private void Spawn(Enemy myEnemy)
    {
        Enemy newEnemy = Instantiate(myEnemy, transform.position, transform.rotation) as Enemy;
        newEnemy.transform.parent = transform;
        newEnemy.SetDmg(levelController.GetLevel() + newEnemy.GetDmg());
    }

    public void StopSpawning()
    {
        spawn = false;
    }
}
