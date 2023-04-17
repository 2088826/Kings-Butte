using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    
    public void Play() 
    {
        //Might need to change the name of the level or the function to work with index.
        SceneManager.LoadScene("ProtoPlayScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
