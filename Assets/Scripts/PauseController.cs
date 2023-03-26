using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameStateToggle();
        }
    }
    public void ToggleColor()
    {
        var test = GameObject.FindGameObjectWithTag("Color");
        if(test != null)
        {
            test.active = !test.active;
        }
    }
    public void gameStateToggle()
    {
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.Gameplay
            ? GameState.Paused
            : GameState.Gameplay;

        GameStateManager.Instance.SetState(newGameState);
    }
}
