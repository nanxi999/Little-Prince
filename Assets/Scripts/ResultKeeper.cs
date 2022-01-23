using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ResultKeeper : MonoBehaviour
{
    private Dictionary<string, Dictionary<string, int>> gameResults;
    private int gameLevel;
    private HighScore highScore;
    /*
    [SerializeField] private List<string> names;
    [SerializeField] private List<int> scores;
    [SerializeField] private List<int> rescues;
    [SerializeField] private List<int> deaths;*/

    // Start is called before the first frame update
    private void Awake()
    {
        ResultKeeper[] resultKeeper = FindObjectsOfType<ResultKeeper>();

        if (resultKeeper.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        ResetGameResult();
        LoadHighScore();
    }

    public void ResetGameResult()
    {
        gameResults = new Dictionary<string, Dictionary<string, int>>();
    }

    public void AddPrince(Prince prince)
    {
        Dictionary<string, int> result = new Dictionary<string, int>();
        result.Add("scores", prince.GetScore());
        result.Add("rescues", prince.GetRescues());
        result.Add("deaths", prince.GetDeaths());

        gameResults.Add(prince.GetName(), result);
    }

    public void SetGameLevel(int level)
    {
        this.gameLevel = level;
    }

    public Dictionary<string, Dictionary<string, int>> GetResults()
    {
        return gameResults;
    }

    public int GetGameLevel()
    {
        return gameLevel;
    }

    public void SaveHighScore()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/high_score.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, highScore);
        stream.Close();
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/high_score.dat";

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            highScore = formatter.Deserialize(stream) as HighScore;
            stream.Close();
        } 
        catch (Exception e)
        {
            highScore = new HighScore();
        }
    }

    public void UpdateHighScore(int level, int score)
    {
        if (level > highScore.level)
            highScore.level = level;
        if (score > highScore.score)
            highScore.score = score;

        SaveHighScore();
    }

    public HighScore GetHighScore()
    {
        return highScore;
    }

    /*
    public List<string> GetPrinceNames()
    {
        return names;
    }

    public List<int> GetPrinceScores()
    {
        return scores;
    }

    public List<int> GetDeaths()
    {
        return deaths;
    }

    public List<int> GetRescues()
    {
        return rescues;
    }*/
}