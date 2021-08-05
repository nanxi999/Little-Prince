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
    public GameObject firstSelectedOnPauseDefault;
    public GameObject firstSelectedOnPauseHelp;
    public GameObject countDownDisplay;

    private bool pause= false;
    private EventSystem es;
    private Animator instructionAnimator;

    private void Awake()
    {
        instruction = transform.Find("Instruction").gameObject;
        instructionAnimator = instruction.GetComponent<Animator>();
        pauseMenu = transform.Find("Paused Menu").gameObject;
        es = GetComponent<EventSystem>();
    }

    public IEnumerator ShowInstruction(string content, float duration)     //Should be called in coroutine
    {
        InstructionUp();
        instruction.GetComponent<TMP_Text>().text = content;
        yield return new WaitForSeconds(duration);
        InstructionDown();
    }

    private void InstructionUp()
    {
        instructionAnimator.SetTrigger("Up");
        return;
    }

    private void InstructionDown()
    {
        instructionAnimator.SetTrigger("Down");
        return;
    }

    public void DisplayCountDown(bool status, int remaininTime)
    {
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
            if(firstSelectedOnPauseDefault)
            {
                es.SetSelectedGameObject(firstSelectedOnPauseDefault, new BaseEventData(es));
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

    public void MainButtonOnClick()
    {
        FindObjectOfType<SceneLoader>().LoadSceneWithIndex(0);
        TogglePauseStatus();
    }

    public void PauseMenuHelpPage()
    {
        foreach(Transform page in pauseMenu.transform)
        {
            if(page.gameObject.name == "HelpPage")
            {
                page.gameObject.SetActive(true);
                es.SetSelectedGameObject(firstSelectedOnPauseHelp, new BaseEventData(es));
            } else
            {
                page.gameObject.SetActive(false);
            }
        }
    }

    public void PauseMenuDefaultPage()
    {
        foreach (Transform page in pauseMenu.transform)
        {
            if (page.gameObject.name == "DefaultPage")
            {
                page.gameObject.SetActive(true);
                es.SetSelectedGameObject(firstSelectedOnPauseDefault, new BaseEventData(es));
            }
            else
            {
                page.gameObject.SetActive(false);
            }
        }
    }
}
