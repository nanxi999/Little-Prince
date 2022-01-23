using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText; 
    [SerializeField] TMP_Text levelText;

    private void Start()
    {
        ShowHighScore();
    }

    private void ShowHighScore()
    {
        ResultKeeper resultKeeper = FindObjectOfType<ResultKeeper>();
        scoreText.SetText(resultKeeper.GetHighScore().score.ToString());
        levelText.SetText(resultKeeper.GetHighScore().level.ToString());
    }
}
