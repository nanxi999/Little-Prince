using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results : MonoBehaviour
{
    [SerializeField] private List<string> names;
    [SerializeField] private List<int> scores;
    [SerializeField] private List<int> rescues;
    [SerializeField] private List<int> deaths;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPrince(Prince prince)
    {
        names.Add(prince.GetName());
        scores.Add(prince.GetScore());
        deaths.Add(prince.GetDeaths());
        rescues.Add(prince.GetRescues());
    }

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
    }
}