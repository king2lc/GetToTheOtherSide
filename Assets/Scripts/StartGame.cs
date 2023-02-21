using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    GameObject[] pauseObjects;
    public string levelName;

    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameStateManager.Instance.CurrentGameState);
    }

    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Gameplay)
            hidePaused();
        else
            showPaused();
    }

    void Update()
    {
    }

    public void Resume()
    {
        GameStateToggle();
    }

    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        GameStateToggle();
    }

    public void LoadMainMenu()
    {
        GameStateToggle();
        SceneManager.LoadScene("TitleScreen");
    }   


    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    private void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }
    private void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void GameStateToggle()
    {
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.Gameplay
            ? GameState.Paused
            : GameState.Gameplay;

        GameStateManager.Instance.SetState(newGameState);
    }

}
