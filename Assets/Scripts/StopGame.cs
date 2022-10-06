using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGame : MonoBehaviour
{
    public void EndGame()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

        Application.Quit();
    }
}
