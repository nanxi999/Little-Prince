using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScore
{
    public int level;
    public int score;

    public HighScore()
    {
        this.level = 0;
        this.score = 0;
    }
}
