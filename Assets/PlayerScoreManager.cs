using UnityEngine;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;

    public TMP_Text player1ScoreText; // Direct reference to TMP_Text component for player 1
    public TMP_Text player2ScoreText; // Direct reference to TMP_Text component for player 2

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

    void UpdateScoreUI()
    {
        if (player1ScoreText != null)
        {
            player1ScoreText.text = "Player 1: " + player1Score;
            Debug.Log("Player 1 Score Text Updated: " + player1ScoreText.text);
        }
        else
        {
            Debug.LogError("Player 1 Score Text is null");
        }

        if (player2ScoreText != null)
        {
            player2ScoreText.text = "Player 2: " + player2Score;
            Debug.Log("Player 2 Score Text Updated: " + player2ScoreText.text);
        }
        else
        {
            Debug.LogError("Player 2 Score Text is null");
        }
    }
}
