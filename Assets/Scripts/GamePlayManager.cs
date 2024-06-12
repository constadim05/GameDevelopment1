using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayManager : MonoBehaviour
{
    public enum State { Intro, Gameplay, Pause, Ending }

    [Header("Game Settings")]
    public State gameState = State.Intro;
    public int player1Score, player2Score;
    public int maxScore;
    public float gameDuration;

    [Header("Player Settings")]
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    [HideInInspector] public GameObject player1;
    [HideInInspector] public GameObject player2;
    public string player1Name, player2Name;

    [Header("UI Settings")]
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;
    public TMP_Text timerText;

    [Header("Other Components")]
    public Timer gameTimer;

    public static GamePlayManager instance;

    private void Awake()
    {
        instance = this;
    }

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

    void Update()
    {
        if (gameState == State.Gameplay)
        {
            DisplayTimer();
        }
    }

    public void UpdateScore(int playerNumber, int amount)
    {
        if (gameState != State.Gameplay) return;

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

        if ((player1Score >= maxScore || player2Score >= maxScore) && gameState != State.Ending)
        {
            EndGame(player1Score >= maxScore ? 1 : 2);
            gameState = State.Ending;
        }

        player1ScoreText.text = player1Name + " : " + player1Score;
        player2ScoreText.text = player2Name + " : " + player2Score;
    }

    void DisplayTimer()
    {
        timerText.text = gameTimer.GetFormattedTime();
    }

    void SetupGame()
    {
        gameState = State.Gameplay;
        player1ScoreText.text = player1Name + " : " + 0;
        player2ScoreText.text = player2Name + " : " + 0;
        gameTimer.countDown = true;
        gameTimer.maxTime = gameDuration;
        gameTimer.StartTimer();
    }

    public void EndGame(int winningPlayerNumber)
    {
        player1.GetComponent<CCMovement>().enabled = false;
        player2.GetComponent<CCMovement>().enabled = false;
        gameState = State.Ending;
        player1ScoreText.text = "";
        player2ScoreText.text = "";
        timerText.text = "";

        string winningPlayer = winningPlayerNumber == 1 ? player1Name : player2Name;

        GameMaster.instance.SortTempList(GameMaster.instance.tempPlayers, true);
        GameMaster.instance.SaveGame();

        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.ShowEndGameText(winningPlayerNumber == 1);
        }
        else
        {
            LoadMainMenu();
        }
    }

    void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
