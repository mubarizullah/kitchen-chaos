using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event EventHandler OnStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private enum StateOfGame
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    private StateOfGame stateOfGame;

    [SerializeField] float waitingToStartTimer = 1f;
    [SerializeField] public float countDownTimer = 3f;
    [SerializeField] float gamePlayingTimer ;
    [SerializeField] float gamePlayingTimerMax = 10f;

    private void Update()
    {
        switch (stateOfGame)
        {
            case StateOfGame.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    stateOfGame = StateOfGame.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case StateOfGame.CountDownToStart:
                countDownTimer -= Time.deltaTime;
                if (countDownTimer < 0f)
                {
                    stateOfGame = StateOfGame.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case StateOfGame.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    stateOfGame = StateOfGame.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case StateOfGame.GameOver:
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
        }

        Debug.Log(stateOfGame);
    }

    public bool IsGamePlaying()
    {
        return stateOfGame == StateOfGame.GamePlaying;
    }

    public bool IsCountDownTimerActive()
    {
        return stateOfGame == StateOfGame.CountDownToStart;
    }

    public float GetCountDown()
    {
        return countDownTimer;
    }

    public bool IsGameOverState()
    {
        return stateOfGame == StateOfGame.GameOver;
    }
    public float GetNormalizedGamePlayTimer()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }


}
