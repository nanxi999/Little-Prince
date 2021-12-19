using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TMP_Text scoreboard;
    [SerializeField] TMP_Text topScore;
    int max = 0;
    string leader = "no leader yet";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Prince[] princes = FindObjectsOfType<Prince>();
        for (int i = 0; i < princes.Length; i++)
        {
            int score = princes[i].GetScore();
            if(score > max)
            {
                max = score;
                leader = princes[i].GetName();
                scoreboard.text = "Kill Leader: " + leader;
                topScore.text = "Score: " + max.ToString();
            }
        }
    }
}
