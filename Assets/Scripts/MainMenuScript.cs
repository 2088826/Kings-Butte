using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject StagePanel;
    [SerializeField] AnimationClip openStagePanel;
    [SerializeField] AnimationClip closeStagePanel;
    public void PlayButton() 
    {
        //if StagePanel is visible then hide it
        if (StagePanel.activeSelf)
        {
            StagePanel.SetActive(false);
        }
        else
        {
            //Play the openStagePanel animation

            //Set stage panel to visible
            StagePanel.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
