using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Summary : MonoBehaviour
{
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
    }
}
