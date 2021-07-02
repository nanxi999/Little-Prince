﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    int level = 1;
    int levelTime;
    int numberOfAttackers;
    bool levelTimeFinished;

    private GameUI gameUI;

    private void Awake()
    {
        InitializeLevel();
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
        else if(levelTime < 60)
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
            StartCoroutine(NextLevel());
        }
    }

    public IEnumerator NextLevel()
    {
        StartCoroutine(gameUI.ShowInstruction("Level completed! Get ready for the next level..."));
        level += 1;
        yield return new WaitForSeconds(3);
        StartCoroutine(gameUI.ShowInstruction("Level " + level));
        yield return new WaitForSeconds(3);
        InitializeLevel();
    }

    private void StartSpawners()
    {
        ShadowSpawner[] spawnerArray = FindObjectsOfType<ShadowSpawner>();
        foreach (ShadowSpawner spawner in spawnerArray)
        {
            StartCoroutine(spawner.StartSpawning());
        }
    }

    private void StopSpawners()
    {
        ShadowSpawner[] spawnerArray = FindObjectsOfType<ShadowSpawner>();
        foreach (ShadowSpawner spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }

    public void EnemySpawned()
    {
        numberOfAttackers++;
    }

    public void EnemyKilled()
    {
        numberOfAttackers--;
        Debug.Log(numberOfAttackers);
        if (numberOfAttackers <= 0 && levelTimeFinished)
        {
            StartCoroutine(NextLevel());
        }
    }
}