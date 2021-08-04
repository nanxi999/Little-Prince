using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Cinemachine;

public class LevelController : MonoBehaviour
{
    [SerializeField] int level = 1;
    [SerializeField] int levelTime;
    int numberOfAttackers;
    int numberOfPlayers;
    List<int> playersAwarded;
    bool levelTimeFinished;
    Ammunition[] ammu;

    [SerializeField] public Transform startCameraPos;
    [SerializeField] public GameObject joinText;
    // next level count down
    [SerializeField] private float prepareTime = 15f;
    [SerializeField] private TMP_Text enemyCountText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private int enemyToSpawn = 25;

    private float timeBeforeNextLv;
    private bool prepSession = false;
    private int enemyToKill;
    private GameUI gameUI;

    private void Awake()
    {
        FindObjectOfType<CinemachineTargetGroup>().AddMember(startCameraPos, 1, 0);
        timeBeforeNextLv = prepareTime;
        numberOfPlayers = 0;
        playersAwarded = new List<int>();
        ammu = FindObjectsOfType<Ammunition>();
    }

    private void Update()
    {
        PrepSessionCountDown();
        enemyCountText.text = numberOfAttackers.ToString();
    }

    private void PrepSessionCountDown()
    {
        if (prepSession)
        {
            timeBeforeNextLv -= Time.deltaTime;
            gameUI.DisplayCountDown(true, Mathf.FloorToInt(timeBeforeNextLv));

            if (timeBeforeNextLv <= 0)
            {
                timeBeforeNextLv = 0;
                prepSession = false;
                gameUI.DisplayCountDown(false, 0);
                StartCoroutine(NextLevel());
            }
        }
    }

    private void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
        levelText.text = "Level " + level;
    }

    public int GetLevel()
    {
        return level;
    }

    public void InitializeLevel()
    {
        if(level == 1)
            levelTime = 20;
        else if(level%5 == 0 )
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
        StartCoroutine(gameUI.ShowInstruction("Level completed! Get ready for the next level...", 3f));
        FindObjectOfType<AwardsSpawner>().StartSpawning();
        Prince[] players = FindObjectsOfType<Prince>();
        foreach(Prince p in players)
        {
            p.SetHealth(p.GetMaxHealth());
            StatsManager stats = p.GetComponent<StatsManager>();
            stats.ResetPassiveSkills();
            stats.ClearFactors();
            stats.SetBulletId(0);
        }

        // set the ammo supply point as active
        foreach(Ammunition ammo in ammu)
        {
            ammo.SetAmmuActive(true);
        }

        // prep session starts
        timeBeforeNextLv = prepareTime;
        prepSession = true;
    }

    public IEnumerator NextLevel()      //Called when the count down timer reaches 0
    {
        DestroyAwards();
        level += 1;
        levelText.text = "Level " + level;
        StartCoroutine(gameUI.ShowInstruction("Level " + level, 3f));
        yield return new WaitForSeconds(2);
        foreach (Ammunition ammo in ammu)
        {
            ammo.SetAmmuActive(false);
        }
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

    private void DestroyAwards()
    {
        var awards = FindObjectsOfType<Award>();
        foreach (Award i in awards)
        {
            Destroy(i.gameObject);
        }
    }

    /*
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
            //StartCoroutine(NextLevel());
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
    */

    public void GameOverCheck()
    {
        Prince[] players = FindObjectsOfType<Prince>();
        bool gameOver = true;
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
        {
            joinText.SetActive(false);
            InitializeLevel();
            FindObjectOfType<CinemachineTargetGroup>().RemoveMember(startCameraPos);
        }
    }

    public void PlayerLeft()
    {
        numberOfPlayers -= 1;
    }

    public void TogglePauseGame()
    {
        FindObjectOfType<GameUI>().TogglePauseStatus();
    }
}
