using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject PlayButton;
    [SerializeField] GameObject TutorialButton;
    [SerializeField] GameObject CreditButton;
    [SerializeField] GameObject ExitButton;
    [SerializeField] GameObject StageBackButton;
    [SerializeField] GameObject CreditBackButton;
    [SerializeField] GameObject Stage1;
    [SerializeField] GameObject Stage2;
    [SerializeField] GameObject Stage3;

    public void OpenMenu() 
    {
        //Disable the buttons
        PlayButton.SetActive(false);
        TutorialButton.SetActive(false);
        CreditButton.SetActive(false);
        ExitButton.SetActive(false);
    }
  
    public void CloseMenu()
    {
        //Reavtivate the Disable the buttons
        PlayButton.SetActive(true);
        TutorialButton.SetActive(true);
        CreditButton.SetActive(true);
        ExitButton.SetActive(true);
    }

    public void GoToStage1()
    {
        //Add transition goto Stage1 scene.
    }

    public void GoToStage2()
    {
        //Add transition goto Stage2 scene.
    }

    public void GoToStage3()
    {
        //Add transition goto Stage3 scene.
    }

    public void GoToTutorial()
    {
        //Add transition goto Tutorial scene.
    }
    public void ExitButtonClick()
    {
        //Add transition.
        Application.Quit();
    }
}
