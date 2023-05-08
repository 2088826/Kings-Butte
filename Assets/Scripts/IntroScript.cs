using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAction;
    private InputActionMap uiActionMap;

    public void Start()
    {
        uiActionMap = inputAction.FindActionMap("UI");

    }
    private void Update()
    {
        if(uiActionMap != null)
        {
            if (uiActionMap["Start"].triggered)
            {

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
