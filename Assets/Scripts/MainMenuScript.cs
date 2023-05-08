using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject TutorialButton;
    public GameObject CreditButton;
    public GameObject ExitButton;
    public GameObject StageBackButton;
    public GameObject CreditBackButton;
    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;

    public void InvokeOpenMenu()
    {
        Invoke(nameof(OpenMenu), 0.5f);
    }
    public void OpenMenu() 
    {
        //Disable the buttons & Sound
        PlayButton.GetComponent<Button>().interactable = false;
        PlayButton.GetComponent<AudioSource>().volume = 0;

        TutorialButton.GetComponent<Button>().interactable = false;
        TutorialButton.GetComponent<AudioSource>().volume = 0;

        CreditButton.GetComponent<Button>().interactable = false;
        CreditButton.GetComponent<AudioSource>().volume = 0;

        ExitButton.GetComponent<Button>().interactable = false;
        ExitButton.GetComponent<AudioSource>().volume = 0;
    }

    public void InvokeCloseMenu()
    {
        Invoke(nameof(CloseMenu), 0.5f);
    }
    public void CloseMenu()
    {
        //Reavtivate the Disable the buttons
        PlayButton.GetComponent<Button>().interactable = true;
        PlayButton.GetComponent<AudioSource>().volume = 1;

        TutorialButton.GetComponent<Button>().interactable = true;
        TutorialButton.GetComponent<AudioSource>().volume = 1;

        CreditButton.GetComponent<Button>().interactable = true;
        CreditButton.GetComponent<AudioSource>().volume = 1;

        ExitButton.GetComponent<Button>().interactable = true;
        ExitButton.GetComponent<AudioSource>().volume = 1;
    }
    public void InvokeGoToStage1()
    {
        Invoke(nameof(GoToStage1), 1);
    }
    public void GoToStage1()
    {
        //Add transition goto Stage1 scene.
        SceneManager.LoadScene("ProtoPlayScene");
    }
    public void InvokeGoToStage2()
    {
        Invoke(nameof (GoToStage2), 1);
    }
    public void GoToStage2()
    {
        //Add transition goto Stage2 scene.
        SceneManager.LoadScene("Level2");
    }

    public void GoToStage3()
    {
        //Add transition goto Stage3 scene.
    }
    public void InokeTutorial()
    {
        Invoke(nameof(GoToTutorial), 1);

        Debug.Log("Moved Scene");
    }
    public void GoToTutorial()
    {
        //Goto to scene 3.
        SceneManager.LoadScene("Tutorial");
    }

    public void InvokeExit()
    {
        Invoke(nameof(ExitButtonClick), 0.5f);
    }
    public void ExitButtonClick()
    {
        //Close the game.
        Application.Quit();
    }

    public void SetStage1()
    {
        if(EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(Stage1);
        }
    }

    public void SetPlay()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(PlayButton);
        }
    }

    public void SetCreditMenu()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(CreditBackButton);
        }
    }

    public void SetCreditClose()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(CreditButton);
        }
    }
}
