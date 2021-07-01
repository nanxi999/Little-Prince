using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    float startTime = 0;
    float levelTime = 1;
    bool levelTimeFinished = false;

    void Update()
    {
        if (levelTimeFinished) { return; }
        GetComponent<Slider>().value = (Time.timeSinceLevelLoad - startTime) / levelTime;

        if (Time.timeSinceLevelLoad - startTime >= levelTime)
        {
            levelTimeFinished = true;
            FindObjectOfType<LevelController>().LevelTimerFinished();
        }
    }

    public void ResetTimer(float time)
    {
        startTime = Time.timeSinceLevelLoad;
        levelTimeFinished = false;
        levelTime = time;
    }
}
