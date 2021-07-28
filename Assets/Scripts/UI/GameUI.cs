using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{
    public GameObject instruction;
    public GameObject pauseMenu;
    public GameObject firstSelectedOnPause;
    public GameObject countDownDisplay;

    private bool pause= false;
    private EventSystem es;

    private void Awake()
    {
        instruction = transform.Find("Instruction").gameObject;
        pauseMenu = transform.Find("Paused Menu").gameObject;
        es = GetComponent<EventSystem>();
    }

    public IEnumerator ShowInstruction(string content)     //Should be called in coroutine
    {
        instruction.SetActive(true);
        instruction.GetComponent<TMP_Text>().text = content;
        yield return new WaitForSeconds(3);
        instruction.SetActive(false);
    }

    public void DisplayCountDown(bool status, int remaininTime)
    {
        Debug.Log("count");
        countDownDisplay.SetActive(status);
        if(status)
        {
            countDownDisplay.GetComponent<TMP_Text>().text = remaininTime + "s before the next level";
        }
    }

    public void TogglePauseStatus()
    {
        pause = !pause;
        if(pause)
        {
            Time.timeScale = 0f;
            Prince[] allPrince = FindObjectsOfType<Prince>();
            foreach(Prince prince in allPrince) {
                prince.ToggleInputStatus();
            }
            pauseMenu.SetActive(true);
            if(firstSelectedOnPause)
            {
                es.SetSelectedGameObject(firstSelectedOnPause, new BaseEventData(es));
            }
        }
        else
        {
            Prince[] allPrince = FindObjectsOfType<Prince>();
            foreach (Prince prince in allPrince)
            {
                prince.ToggleInputStatus();
            }
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }
}
