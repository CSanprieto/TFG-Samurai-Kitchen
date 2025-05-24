using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    // Make game states
    private enum State{
        waitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float CountdownToStartTimer = 3f;
    private float GamePlayingTimer;
    private float GamePlayingTimerMax = 180f;

    public static KitchenGameManager Instance {private set; get;}

    public event EventHandler OnStateChanged;

    private void Awake()
    {
        Instance = this;
        state = State.waitingToStart;
    }

    private void Update()
    {
        switch(state){
            case State.waitingToStart:
            waitingToStartTimer -= Time.deltaTime;
            if(waitingToStartTimer < 0f){
                state = State.CountdownToStart;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;

            case State.CountdownToStart:
            CountdownToStartTimer -= Time.deltaTime;
            if(CountdownToStartTimer < 0f){
                state = State.GamePlaying;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                GamePlayingTimer = GamePlayingTimerMax;
            }
            break;

            case State.GamePlaying:
            GamePlayingTimer -= Time.deltaTime;
            if(GamePlayingTimer < 0f){
                state = State.GameOver;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;

            case State.GameOver:
            break;
        }

        Debug.Log(state);
    }


    public bool IsGamePlaying(){
        return state == State.GamePlaying;
    }

    public bool IsGameInCountDownToStart(){
        return state == State.CountdownToStart;
    }

    public bool IsGameOver(){
        return state == State.GameOver;
    }

    public float GetGameTimerNormalized(){
        return 1 -  (GamePlayingTimer / GamePlayingTimerMax);
    }
}
