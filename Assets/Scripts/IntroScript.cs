using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAction;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button continueButton;

    private BtnSound btnSound;
    private InputActionMap uiActionMap;
    private string scene;

    public void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        btnSound = GetComponent<BtnSound>();
        if(inputAction != null)
        {
            uiActionMap = inputAction.FindActionMap("UI");
        }
    }
    private void Update()
    {
        if(uiActionMap != null)
        {
        
            if(scene == "Tutorial")
            {
                if (uiActionMap["Cancel"].triggered)
                {
                    if(skipButton != null)
                    {
                        btnSound.ClickSound();
                        skipButton.onClick.Invoke();
                    }
                }
            }
            else
            {
                if (uiActionMap["Start"].triggered)
                {
                    if (skipButton.gameObject.activeSelf)
                    {
                        if (skipButton != null)
                        {
                            btnSound.ClickSound();
                            skipButton.onClick.Invoke();
                        }
                    }
                    else
                    {
                        if (continueButton != null)
                        {
                            btnSound.ClickSound();
                            continueButton.onClick.Invoke();
                        }
                    }
                }
            

                if (continueButton.gameObject.activeSelf)
                {
                    if(skipButton != null)
                    {
                        skipButton.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    //Invoke Goto
    public void InvokeMainMenu()
    {
        Invoke(nameof(LoadMainMenu), 1.6f);
    }

    // Invoke Restart
    public void InvokeRestart()
    {
        Invoke(nameof(Restart), 1.6f);
    }
    
    //Goto ProtoMainMenu
    public void LoadMainMenu()
    {
        //Load the scene
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    // Restart the scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
