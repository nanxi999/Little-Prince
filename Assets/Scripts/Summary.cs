using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Summary : MonoBehaviour
{
    [SerializeField] private ResultKeeper resultKeeper;
    [SerializeField] private PlayerResult playerResultPrefab;
    [SerializeField] private TMP_Text gameLevel;
    [SerializeField] private Dictionary<string, Dictionary<string, int>> gameResults;

    private void Start()
    {
        resultKeeper = FindObjectOfType<ResultKeeper>();
        gameResults = resultKeeper.GetResults();
        ShowResults();
        ShowGameLevel();
    }

    private void ShowGameLevel()
    {
        gameLevel.SetText("Your level: " + resultKeeper.GetGameLevel());
    }

    private void ShowResults()
    {
        int playerCount = gameResults.Count;
        float[] gaps = { 0f, 5f, 10f, 13f };
        float gap;
        float left;

        /* Control the gap distance between each player result */
        if (playerCount > 1)
        {
            gap = gaps[playerCount - 1] / (playerCount - 1);
            left = gaps[playerCount - 1] / 2;
        } else
        {
            gap = left = 0;
        }

        /* Sort the results by score */
        var orderedResults = gameResults.OrderByDescending(entry => entry.Value["scores"]);

        /* Show the results for each player */
        int ranking = 1;
        foreach (KeyValuePair<string, Dictionary<string,int>> entry in orderedResults) {
            
            PlayerResult result = (PlayerResult)Instantiate(playerResultPrefab,
                new Vector3(-left + (ranking - 1) * gap, 1.6f, -5), transform.rotation);

            result.setResult(entry.Key,
                "Scores: " + entry.Value["scores"].ToString(),
                "Rescues: " + entry.Value["rescues"].ToString(),
                "Deaths: " + entry.Value["deaths"].ToString(),
                RankingToString(ranking));

            ranking += 1;
        }
    }

    private string RankingToString(int ranking)
    {
        switch(ranking)
        {
            case 1:
                return "1st";
            case 2:
                return "2nd";
            case 3:
                return "3rd";
            case 4:
                return "4th";
            default:
                return null;
        }
    }

    /*
    // Start is called before the first frame update
    [SerializeField] private TMP_Text text_score;
    [SerializeField] private TMP_Text text_rescue;
    [SerializeField] private TMP_Text text_deaths;
    [SerializeField] private TMP_Text text_name;

    private int index = 0;
    private List<string> names;
    private List<int> scores;
    private List<int> rescues;
    private List<int> deaths;
    private bool waiting = false;

    private int mvpIndex = -1;

    Results resultManager;

    
    void Start()
    {
        resultManager = FindObjectOfType<Results>();
        names = resultManager.GetPrinceNames();
        rescues = resultManager.GetRescues();
        deaths = resultManager.GetDeaths();
        scores = resultManager.GetPrinceScores();
    }

    // Update is called once per frame
    void Update()
    {
        if(mvpIndex == -1)
        {
            initializeData();
        } else if(!waiting)
        {
            waiting = true;
            StartCoroutine(Wait());
        }
    }

    private void initializeData()
    {
        int maxScore = 0;
        for(int i = 0; i < names.Count; i++)
        {
            if(scores[i] > maxScore)
            {
                maxScore = scores[i];
                mvpIndex = i;
            }
        }

        text_score.text = "Score: " + maxScore.ToString();
        text_rescue.text = "Rescues: " + rescues[mvpIndex].ToString();
        text_deaths.text = "Deaths: " + deaths[mvpIndex].ToString();
        text_name.text = "MVP: " + names[mvpIndex];
        resultText[] texts = GetComponentsInChildren<resultText>();
        foreach (resultText text in texts)
        {
            text.StartFading();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        createInfoForNextPlayer();
    }
    private void createInfoForNextPlayer()
    {
        if(index == names.Count)
        {
            return;
        }
        for(int i = index; i < names.Count; i++)
        {
            if(i == mvpIndex)
            {
                index = i + 1;
                continue;
            } else
            {
                index = i + 1;
                text_score.text = "Score: " + scores[i].ToString();
                text_rescue.text = "Rescues: " + rescues[i].ToString();
                text_deaths.text = "Deaths: " + deaths[i].ToString();
                text_name.text = "Player: " + names[i];

                resultText[] texts = GetComponentsInChildren<resultText>();
                Debug.Log(texts.Length);
                foreach(resultText text in texts)
                {
                    text.Appear();
                }
                break;
            }
        }
        waiting = false;
    }*/
}
