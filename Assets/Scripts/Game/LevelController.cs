using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class LevelController : MonoBehaviour
{
    int level = 1;
    int levelTime;
    int numberOfAttackers;
    int numberOfPlayers;
    List<int> playersAwarded;
    bool levelTimeFinished;

    private GameUI gameUI;

    private void Awake()
    {
        numberOfPlayers = 0;
        playersAwarded = new List<int>();
    }

    private void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
    }

    public int GetLevel()
    {
        return level;
    }

    public void InitializeLevel()
    {
        if(level == 1)
            levelTime = 10;
        else if(levelTime < 40)
            levelTime += 5;
        StartSpawners();
        numberOfAttackers = 0;
        levelTimeFinished = false;
        FindObjectOfType<GameTimer>().ResetTimer(levelTime);
    }

    public void LevelTimerFinished()
    {
        levelTimeFinished = true;
        StopSpawners();
        if (numberOfAttackers <= 0)
        {
            CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
        StartCoroutine(gameUI.ShowInstruction("Level completed! Get ready for the next level..."));
        level += 1;
        FindObjectOfType<AwardsSpawner>().StartSpawning();
        Prince[] players = FindObjectsOfType<Prince>();
        foreach(Prince p in players)
        {
            p.SetHealth(p.GetMaxHealth());
            StatsManager stats = p.GetComponent<StatsManager>();
            stats.ResetPassiveSkills();
            stats.SetBulletId(0);
        }
    }

    public IEnumerator NextLevel()      //Called when all players are awarded.
    {
        StartCoroutine(gameUI.ShowInstruction("Level " + level));
        yield return new WaitForSeconds(3);
        InitializeLevel();
    }

    private void StartSpawners()
    {
        EnemySpawner[] spawnerArray = FindObjectsOfType<EnemySpawner>();
        foreach (EnemySpawner spawner in spawnerArray)
        {
            StartCoroutine(spawner.StartSpawning());
        }
    }

    private void StopSpawners()
    {
        EnemySpawner[] spawnerArray = FindObjectsOfType<EnemySpawner>();
        foreach (EnemySpawner spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }

    public void EnemySpawned()
    {
        numberOfAttackers++;
        //Debug.Log("number of enemies:" + numberOfAttackers);
    }

    public void EnemyKilled()
    {
        numberOfAttackers--;
        //Debug.Log("number of enemies:" + numberOfAttackers);
        if (numberOfAttackers <= 0 && levelTimeFinished)
        {
            numberOfAttackers = 0;
            CompleteLevel();
        }
    }

    public void PlayerAwarded(int id)
    {
        playersAwarded.Add(id);
        if(playersAwarded.Count >= Mathf.Min(numberOfPlayers, 3))
        {           
            var awards = FindObjectsOfType<Award>();
            foreach(Award i in awards)
            {
                Destroy(i.gameObject);
            }
            StartCoroutine(NextLevel());
            ResetAwarded();
        }
    }

    public void ResetAwarded()
    {
        playersAwarded.Clear();
    }

    public bool PlayerIsAwarded(int id)
    {
        return playersAwarded.Contains(id);
    }

    public void GameOverCheck()
    {
        Prince[] players = FindObjectsOfType<Prince>();
        bool gameOver = true;
        Debug.Log("number of princes remaining:" + players.Length);
        foreach(Prince p in players)
        {
            if (!p.IsCryin())
                gameOver = false;
        }
        Debug.Log(gameOver);
        if (gameOver)
            StartCoroutine(GameOver());    
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<SceneLoader>().LoadSceneWithIndex(2);
    }

    public void PlayerJoined()
    {
        numberOfPlayers += 1;
        if(numberOfPlayers == 1)
            InitializeLevel();
    }

    public void PlayerLeft()
    {
        numberOfPlayers -= 1;
    }
}
