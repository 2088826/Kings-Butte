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

    public void Start()
    {
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
            if (uiActionMap["Start"].triggered)
            {
                btnSound.ClickSound();
                if (skipButton.gameObject.activeSelf)
                {
                    skipButton.onClick.Invoke();
                }
                else
                {
                    continueButton.onClick.Invoke();
                }
            }

            if (continueButton.gameObject.activeSelf)
            {
                Debug.Log("HOW");
                skipButton.gameObject.SetActive(false);

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
        SceneManager.LoadScene("MainMenu");
    }

    // Restart the scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
