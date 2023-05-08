using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    //Invoke Goto
    public void InvokeGotoProtoMainMenu()
    {
        Invoke(nameof(GotoProtoMainMenu), 1.6f);
    }

    // Invoke Restart
    public void InvokeRestart()
    {
        Invoke(nameof(Restart), 1.6f);
    }
    
    //Goto ProtoMainMenu
    public void GotoProtoMainMenu()
    {
        //Load the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("ProtoMainMenu");
    }

    // Restart the scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
