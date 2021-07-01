using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GameObject instruction;
    public GameObject pauseMenu;

    private void Start()
    {
        instruction = transform.Find("Instruction").gameObject;
        pauseMenu = transform.Find("Paused Menu").gameObject;
    }

    public IEnumerator ShowInstruction(string content)     //Should be called in coroutine
    {
        instruction.SetActive(true);
        instruction.GetComponent<TMP_Text>().text = content;
        yield return new WaitForSeconds(3);
        instruction.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
