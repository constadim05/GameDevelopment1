using UnityEngine;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;

    public TMP_Text player1ScoreText; // Direct reference to TMP_Text component for player 1
    public TMP_Text player2ScoreText; // Direct reference to TMP_Text component for player 2

    public TMP_Text maxKillsText; // Reference to TMP_Text for displaying max kills

    public string player1Name = "Player 1";
    public string player2Name = "Player 2";

    void Start()
    {
        // Check if the TMP_Text components are assigned in the inspector
        if (player1ScoreText == null)
        {
            Debug.LogError("Player1ScoreText is not assigned in the inspector");
        }

        if (player2ScoreText == null)
        {
            Debug.LogError("Player2ScoreText is not assigned in the inspector");
        }

        if (maxKillsText == null)
        {
            Debug.LogError("MaxKillsText is not assigned in the inspector");
        }

        UpdateScoreUI();
    }

    public void UpdatePlayerNames(string newName1, string newName2)
    {
        player1Name = newName1;
        player2Name = newName2;

        UpdateScoreUI();
    }

    public void IncreasePlayerScore(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1Score++;
            Debug.Log("Player 1 Score Increased: " + player1Score);
        }
        else if (playerNumber == 2)
        {
            player2Score++;
            Debug.Log("Player 2 Score Increased: " + player2Score);
        }
        else
        {
            Debug.LogError("Invalid player number: " + playerNumber);
        }

        UpdateScoreUI();
    }

    public void UpdateMaxKillsText(int maxKills)
    {
        if (maxKillsText != null)
        {
            maxKillsText.text = "Max Kills: " + maxKills;
        }
        else
        {
            Debug.LogError("MaxKillsText is not assigned in the inspector");
        }
    }

    void UpdateScoreUI()
    {
        if (player1ScoreText != null)
        {
            player1ScoreText.text = $"{player1Name}: {player1Score}";
            Debug.Log("Player 1 Score Text Updated: " + player1ScoreText.text);
        }
        else
        {
            Debug.LogError("Player 1 Score Text is null");
        }

        if (player2ScoreText != null)
        {
            player2ScoreText.text = $"{player2Name}: {player2Score}";
            Debug.Log("Player 2 Score Text Updated: " + player2ScoreText.text);
        }
        else
        {
            Debug.LogError("Player 2 Score Text is null");
        }
    }
}
