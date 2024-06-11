using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manage game state, scores, and respawns during gameplay. Only one GamePlayManager should be in the scene
/// reference this script to access gameplay objects like UI
/// </summary>
public class GamePlayManager : MonoBehaviour
{
    #region Variables
    //store gameplay states
    public enum State { Intro, Gameplay, Pause, Ending }

    [Header("Game Settings")]
    public State gameState = State.Intro;
    //store score data
    public int player1Score, player2Score;

    public int maxScore;
    public float gameDuration;

    [Header("Player Settings")]
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    //store player object references
    [HideInInspector] public GameObject player1;
    [HideInInspector] public GameObject player2;

    //store player data
    public string player1Name, player2Name;

    [Header("UI Settings")]
    //store UI references
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;
    public TMP_Text timerText;

    [Header("Other Components")]
    //store a timer
    public Timer gameTimer;

    #endregion

    #region Singleton
    public static GamePlayManager instance; //create a static reference to ourselves

    //assign this object to the reference
    private void Awake()
    {
        //if (instance != null) Destroy(gameObject); //optional for if you only ever want 1 singleton
        instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (GameMaster.instance != null)
        {
            maxScore = GameMaster.instance.saveData.maxKills;
            gameDuration = GameMaster.instance.saveData.maxRoundTime;

            player1Name = GameMaster.instance.currentPlayer1.playerName;
            player2Name = GameMaster.instance.currentPlayer2.playerName;
        }
        SetupGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == State.Gameplay)
        {
            DisplayTimer();
        }
    }

    //Score update function - increase one players score, check if game is over, update UI
    public void UpdateScore(int playerNumber, int amount)
    {
        //cancel the function if we are not in gameplay
        if (gameState != State.Gameplay) return;

        //if playerNumber is 1, increase player1 score, otherwise increase player 2
        if (playerNumber == 1)
        {
            GameMaster.instance.currentPlayer1.kills += amount;
            player1Score += amount;
        }
        else
        {
            GameMaster.instance.currentPlayer2.kills += amount;
            player2Score += amount;
        }

        //check if either players score is more than the max score. If so, call End game function
        if ((player1Score >= maxScore || player2Score >= maxScore) && gameState != State.Ending)
        {
            EndGame();
            gameState = State.Ending;
        }
        //update the score UI
        player1ScoreText.text = player1Name + " : " + player1Score;
        player2ScoreText.text = player2Name + " : " + player2Score;
    }

    //Display the timer
    void DisplayTimer()
    {
        timerText.text = gameTimer.GetFormattedTime();
    }

    //run intro sequence
    void SetupGame()
    {
        //set game state
        gameState = State.Gameplay;

        //player names displayed in UI
        player1ScoreText.text = player1Name + " : " + 0;
        player2ScoreText.text = player2Name + " : " + 0;

        //run gameplay timer
        gameTimer.countDown = true;
        gameTimer.maxTime = gameDuration;
        gameTimer.StartTimer();
    }

    //End game - freeze players, tally scores, display results or move to next scene
    public void EndGame()
    {
        //FreezePlayers
        player1.GetComponent<CCMovement>().enabled = false;
        player2.GetComponent<CCMovement>().enabled = false;
        //deactivate other scripts when ready

        //change state
        gameState = State.Ending;

        //update UI;
        player1ScoreText.text = "";
        player2ScoreText.text = "";
        timerText.text = "";

        //display winner
        string winningPlayer;
        if (player1Score > player2Score) winningPlayer = player1Name;
        else winningPlayer = player2Name;

        //messageText.text = winningPlayer + " Wins!" + "\n" + player1Name + " : " + player1Score + "\n" + player2Name + " : " + player2Score;

        GameMaster.instance.SortTempList(GameMaster.instance.tempPlayers, true);
        GameMaster.instance.SaveGame();
    }
}
