using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public static GameManager Instance { get; private set; }
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        Playing,
        GameOver
    }

    private State state;
    //private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float playingTimer;
    [SerializeField] private float playingTimerMax = 30f;
    private bool isGamePaused = false;

    private void Awake()
    {
        state = State.WaitingToStart;
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, new EventArgs());
        }
    }

    private void Update()
    {
        switch (state) { 
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0)
                {
                    state = State.Playing;
                    playingTimer = playingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                playingTimer -= Time.deltaTime;
                if (playingTimer <= 0)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }

    }

    private void GameInput_OnPauseAction(object sender, EventArgs args)
    {
        TogglePauseGame();
    }
    public bool IsGamePlaying()
    {
        return state == State.Playing;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetTimerCountdownNormalized()
    {
        return 1 - (playingTimer / playingTimerMax);
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public void TogglePauseGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1;
            isGamePaused = false;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 0;
            isGamePaused = true;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
