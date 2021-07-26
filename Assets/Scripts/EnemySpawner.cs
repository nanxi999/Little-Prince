using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected float minSpawnDelay = 1f;
    [SerializeField] protected float maxSpawnDelay = 3f;
    [SerializeField] protected float startSpeed = 5f;
    [SerializeField] protected float increaseSpeed = 0.2f;
    [SerializeField] protected Enemy enemyPref;

    protected bool spawn = true;
    protected LevelController levelController;

    protected virtual void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    // Start is called before the first frame update
    public virtual IEnumerator StartSpawning()
    {
        spawn = true;
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            if (spawn)
                SpawnAttacker();
        }
    }

    protected void SpawnAttacker()
    {
        Spawn(enemyPref);
    }

    private void Spawn(Enemy myEnemy)
    {
        Enemy newEnemy = Instantiate(myEnemy, transform.position, transform.rotation) as Enemy;
        newEnemy.transform.parent = transform;
        newEnemy.SetDmg(levelController.GetLevel() + newEnemy.GetDmg());
        newEnemy.SetSpeed((float)(levelController.GetLevel() * increaseSpeed + startSpeed));
    }

    public void StopSpawning()
    {
        spawn = false;
    }
}
