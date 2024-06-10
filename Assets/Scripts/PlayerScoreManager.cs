using UnityEngine;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    public int[] playerScores = new int[2]; // Array to store scores of each player

    public TMP_Text[] playerScoreTexts; // Array to store TMP_Text components for each player

    void Start()
    {
        // Check if all playerScoreTexts are assigned in the inspector
        for (int i = 0; i < playerScoreTexts.Length; i++)
        {
            if (playerScoreTexts[i] == null)
            {
                Debug.LogError("Player" + (i + 1) + "ScoreText is not assigned in the inspector");
            }
        }

        UpdateScoreUI();
    }

    public void IncreasePlayerScore(int playerNumber, int killCount)
    {
        if (playerNumber < 1 || playerNumber > playerScores.Length)
        {
            Debug.LogError("Invalid player number: " + playerNumber);
            return;
        }

        playerScores[playerNumber - 1] += killCount; // Adjusting index for 0-based array

        Debug.Log("Player " + playerNumber + " Score Increased: " + playerScores[playerNumber - 1]);

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        for (int i = 0; i < playerScores.Length; i++)
        {
            if (playerScoreTexts[i] != null)
            {
                playerScoreTexts[i].text = "Player " + (i + 1) + ": " + playerScores[i];
                Debug.Log("Player " + (i + 1) + " Score Text Updated: " + playerScoreTexts[i].text);
            }
            else
            {
                Debug.LogError("Player " + (i + 1) + " Score Text is null");
            }
        }
    }
}
