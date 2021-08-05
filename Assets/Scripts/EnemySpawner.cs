using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected float startMinSpawnDelay = 1f;
    [SerializeField] protected float startMaxSpawnDelay = 3f;
    [SerializeField] protected float baseMinSpawnDelay = 1f;
    [SerializeField] protected float baseMaxSpawnDelay = 3f;
    [SerializeField] protected float spawnDelayDecreaseFactor = 0.1f;
    [SerializeField] protected float speedIncreaseFactor;
    [SerializeField] protected float maxSpeedIncrease;
    [SerializeField] protected float dmgIncreaseFactor;
    [SerializeField] protected float healthIncreaseFactor;
    [SerializeField] protected Enemy enemyPref;

    protected bool spawn = true;
    protected LevelController levelController;
    protected float speedIncrease;
    protected float dmgIncrease;
    protected float healthIncrease;

    protected virtual void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    // Start is called before the first frame update
    public virtual IEnumerator StartSpawning()
    {
        int stage = (levelController.GetLevel() - 1) / 5;
        float minSpawnDelay = Mathf.Max(baseMinSpawnDelay, startMinSpawnDelay - stage * spawnDelayDecreaseFactor);
        float maxSpawnDelay = Mathf.Max(baseMaxSpawnDelay, startMaxSpawnDelay - stage * spawnDelayDecreaseFactor);
        //speedIncrease = Mathf.Min(((levelController.GetLevel() - 1) / 5) * speedIncreaseFactor, maxSpeedIncrease);
        dmgIncrease = ((levelController.GetLevel() - 1) / 5) * dmgIncreaseFactor;
        healthIncrease = ((levelController.GetLevel() - 1) / 5) * healthIncreaseFactor;
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

    protected virtual void Spawn(Enemy myEnemy)
    {
        Enemy newEnemy = Instantiate(myEnemy, transform.position, transform.rotation) as Enemy;
        newEnemy.transform.parent = transform;
        newEnemy.SetDmg(dmgIncrease + newEnemy.GetDmg());
        newEnemy.SetSpeed((float)(speedIncrease + newEnemy.GetSpeed()));
        newEnemy.SetHealth(newEnemy.GetHealth() + healthIncrease);
    }

    public void StopSpawning()
    {
        spawn = false;
    }
}
