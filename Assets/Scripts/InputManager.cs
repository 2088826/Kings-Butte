using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputActionMap ui;
    private InputActionMap join;

    private void Start()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        ui = inputAsset.FindActionMap("UI");
        //join = inputAsset.FindActionMap("Join");
    }

    public void ToggleUI()
    {
        ui.Enable();
        player.Disable();
        //join.Disable();
    }

    public void ToggleJoin()
    {
        //join.Enable();
        ui.Disable();
        player.Disable();
    }

    public void TogglePlayer()
    {
        player.Enable();
        ui.Disable();
        //join.Disable();
    }

    public void ToggleDisable()
    {
        ui.Disable();
        player.Disable();
        //join.Disable();
    }

}
