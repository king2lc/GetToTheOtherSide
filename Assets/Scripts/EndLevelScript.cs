using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadPreviousLevel()
    {
        Debug.Log("Load Previous Level is currently under construction!");
    }
}
 