using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    //Invoke Goto
    public void InvokeGotoProtoMainMenu()
    {
        Invoke(nameof(GotoProtoMainMenu), 1f);
    }
    
    //Goto ProtoMainMenu
    public void GotoProtoMainMenu()
    {
        //Load the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("ProtoMainMenu");
    }
}
