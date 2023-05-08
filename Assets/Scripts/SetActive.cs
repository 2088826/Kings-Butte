using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    [SerializeField] GameObject flashingMessage;
    [SerializeField] GameObject startTimer;
    [SerializeField] GameObject setupScroll;
    [SerializeField] GameObject  buttons;
    [SerializeField] GameObject fade;

    public void SetActiveTrue()
    {
        flashingMessage.SetActive(true);
    }

    public void SetActiveFalse()
    {
        flashingMessage.SetActive(false);
    }

    public void StartTimer()
    {
        startTimer.SetActive(true);
    }

    public void DeactivateScroll()
    {
        setupScroll.SetActive(false);
    }

    public void ActivateButtons()
    {
        buttons.SetActive(true);
    }

    public void DeactivateButtons()
    {
        buttons.SetActive(false);
    }

    public void FadeOut()
    {
        fade.GetComponent<Animator>().SetTrigger("Start");
    }
}
