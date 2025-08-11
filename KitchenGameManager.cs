using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager instance {  get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnPause;
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float resetCountDown = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 20f;
    private bool isGamePasue = false;
    
    private void Awake()
    {
        instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGamePasue();
    }

    private void Update()
    {
        if (GameManager.Instance.isStartGame)
        {
            switch (state)
            {
                case State.WaitingToStart:
                    waitingToStartTimer -= Time.deltaTime;
                    if (waitingToStartTimer < 0f)
                    {
                        state = State.CountdownToStart;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;

                case State.CountdownToStart:
                    countdownToStartTimer -= Time.deltaTime;
                    if (countdownToStartTimer < 0f)
                    {
                        countdownToStartTimer = resetCountDown;
                        state = State.GamePlaying;
                        gamePlayingTimer = gamePlayingTimerMax;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;

                case State.GamePlaying:
                    gamePlayingTimer -= Time.deltaTime;
                    if (gamePlayingTimer < 0f)
                    {
                        state = State.GameOver;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;

                case State.GameOver:
                    countdownToStartTimer -= Time.deltaTime;
                    if (countdownToStartTimer < 0f)
                    {

                       GameManager.Instance.isStartGame = false;
                    }


                    break;
            }
        }
       
      
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountDownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver; 
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void ToggleGamePasue()
    {
        isGamePasue = !isGamePasue;
        if (isGamePasue)
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else 
        {
            Time.timeScale = 1f;
            OnGameUnPause?.Invoke(this, EventArgs.Empty);
        }
       
    }
}

