using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected float startMinSpawnDelay = 1f;
    [SerializeField] protected float startMaxSpawnDelay = 3f;
    [SerializeField] protected float baseMinSpawnDelay = 1f;
    [SerializeField] protected float baseMaxSpawnDelay = 3f;
    [SerializeField] protected float speedIncreaseFactor;
    [SerializeField] protected float maxSpeedIncrease;
    [SerializeField] protected float dmgIncreaseFactor;
    [SerializeField] protected Enemy enemyPref;

    protected bool spawn = true;
    protected LevelController levelController;
    protected float speedIncrease;
    protected float dmgIncrease;

    protected virtual void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    // Start is called before the first frame update
    public virtual IEnumerator StartSpawning()
    {
        float minSpawnDelay = Mathf.Max(baseMinSpawnDelay, startMinSpawnDelay - levelController.GetLevel() * 0.05f);
        float maxSpawnDelay = Mathf.Max(baseMaxSpawnDelay, startMaxSpawnDelay - levelController.GetLevel() * 0.05f);
        speedIncrease = Mathf.Min(((levelController.GetLevel() - 1) / 5) * speedIncreaseFactor, maxSpeedIncrease);
        dmgIncrease = ((levelController.GetLevel() - 1) / 5) * dmgIncreaseFactor;
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
    }

    public void StopSpawning()
    {
        spawn = false;
    }
}
