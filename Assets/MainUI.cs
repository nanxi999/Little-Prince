using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainUI : MonoBehaviour
{
    [SerializeField] GameObject mainPage;
    [SerializeField] GameObject highScorePage;
    [SerializeField] GameObject firstSelectedOnMain;
    [SerializeField] GameObject firstSelectedOnHigh;

    private EventSystem es;

    private void Start()
    {
        es = FindObjectOfType<EventSystem>();
    }

    public void ShowMainPage()
    {
        mainPage.SetActive(true);
        highScorePage.SetActive(false);
        es.SetSelectedGameObject(firstSelectedOnMain, new BaseEventData(es));
    }

    public void ShowHighScorePage()
    {
        
        mainPage.SetActive(false);
        highScorePage.SetActive(true);

        es.SetSelectedGameObject(firstSelectedOnHigh, new BaseEventData(es));
    }


}
