using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject ui;

    private GameObject uiObjects;

    void Start()
    {
        ui = GameObject.Find("GameUICanvas");
        uiObjects = ui.GetComponentInChildren<Transform>().gameObject;
        Debug.Log(uiObjects);
    }

    private void HandlePause()
    {
        pauseMenu.SetActive(true);
    }

    private void HandleResume()
    {
        pauseMenu.SetActive(false);
    }
}
