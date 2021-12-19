using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchWizardSpawner : EnemySpawner
{
    public float dashDmgIncreaseFactor;
    public float laserDmgIncreaseFactor;
    public int firstSpawnLevel = 10;
    private int count = 0;
    private float dashDmgIncrease;
    private float laserDmgIncrease;
    public override IEnumerator StartSpawning()
    {
        speedIncrease = ((levelController.GetLevel() - 1) / 5) * speedIncreaseFactor;
        dmgIncrease = ((levelController.GetLevel() - 1) / 5) * dmgIncreaseFactor;
        dashDmgIncrease = ((levelController.GetLevel() - 1) / 5) * dashDmgIncreaseFactor;
        healthIncrease = ((levelController.GetLevel() - 1 / 5)) * healthIncreaseFactor;
        //laserDmgIncrease = ((levelController.GetLevel() - 1) / 5) * laserDmgIncreaseFactor;
        count = 0;
        if(levelController.GetLevel() % firstSpawnLevel == 0)
        {
            while(count < Mathf.Max((levelController.GetLevel() - 10), 0 ) / (firstSpawnLevel) + 1)
            {
                yield return new WaitForSeconds(Random.Range(startMinSpawnDelay, startMaxSpawnDelay));
                SpawnAttacker();
                count++;
            }
        }
    }

    protected override void Spawn(Enemy myEnemy)
    {
        ArchWizard newEnemy = Instantiate(myEnemy, transform.position, transform.rotation) as ArchWizard;
        newEnemy.transform.parent = transform;
        newEnemy.SetDmg(dmgIncrease + newEnemy.GetDmg());
        newEnemy.SetDashDmg(dashDmgIncrease + newEnemy.GetDashDmg());
        newEnemy.SetSpeed((float)(speedIncrease + newEnemy.GetSpeed()));
        newEnemy.SetHealth(healthIncrease + newEnemy.GetHealth());
    }
}
