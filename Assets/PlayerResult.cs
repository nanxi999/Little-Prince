using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResult : MonoBehaviour
{
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text scores;
    [SerializeField] private TMP_Text rescues;
    [SerializeField] private TMP_Text deaths;
    [SerializeField] private TMP_Text ranking;
    [SerializeField] private GameObject smileRenderer;
    [SerializeField] private GameObject cryRenderer;

    public void setResult(string name, string scores, string rescues, string deaths, string ranking)
    {
        this.name.SetText(name);
        this.scores.SetText(scores);
        this.rescues.SetText(rescues);
        this.deaths.SetText(deaths);
        this.ranking.SetText(ranking);

        switch (ranking)
        {
            case "1st":
                smileRenderer.SetActive(true);
                cryRenderer.SetActive(false);
                this.ranking.colorGradientPreset = new TMP_ColorGradient(
                    new Color32(255, 255, 255, 255),
                    new Color32(255, 255, 255, 255),
                    new Color32(255, 235, 66, 255),
                    new Color32(255, 235, 66, 255)
                );
                break;
            case "2nd":
                cryRenderer.SetActive(true);
                smileRenderer.SetActive(false);
                this.ranking.colorGradientPreset = new TMP_ColorGradient(
                    new Color32(255, 255, 255, 255),
                    new Color32(255, 255, 255, 255),
                    new Color32(200, 200, 200, 255),
                    new Color32(200, 200, 200, 255)
                );
                break;
            case "3rd":
                cryRenderer.SetActive(true);
                smileRenderer.SetActive(false);
                this.ranking.colorGradientPreset = new TMP_ColorGradient(
                    new Color32(255, 255, 255, 255),
                    new Color32(255, 255, 255, 255),
                    new Color32(205, 127, 50, 255),
                    new Color32(205, 127, 50, 255)
                );
                break;
            case "4th":
                cryRenderer.SetActive(true);
                smileRenderer.SetActive(false);
                this.ranking.color = new Color32(255, 255, 255, 255);
                break;
            default:
                break;
        }
    }
}
