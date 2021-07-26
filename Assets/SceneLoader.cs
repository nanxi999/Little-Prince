using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transitionAnimator;

    public void LoadSceneWithIndex(int index)
    {
        StartCoroutine(LoadSceneWithTransition(index));
    }

    public IEnumerator LoadSceneWithTransition(int index)
    {
        transitionAnimator.SetTrigger("Out");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }

    /*
    IEnumerator LoadLevelWithCrossFade(int index)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(index);
    }*/

}
