using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class resultText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private int duration = 2;
    [SerializeField] private string content;

    bool fade = false;
    int count = 0;

    Animator animator;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
        animator = GetComponent<Animator>();
        text.text = content;
        StartFading();
    }

    public void StartFading()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(duration);
        animator.SetTrigger("fade");

    }

    public void Appear()
    {
        animator.SetTrigger("appear");
        StartFading();
    }

}
