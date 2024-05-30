using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;

    public GameObject player1ScoreObject;
    public GameObject player2ScoreObject;

    private Text player1ScoreText;
    private Text player2ScoreText;

    void Start()
    {
        // Get the Text components from the GameObjects
        if (player1ScoreObject != null)
        {
            player1ScoreText = player1ScoreObject.GetComponent<Text>();
            if (player1ScoreText == null)
            {
                Debug.LogError("Player1ScoreText component is missing on " + player1ScoreObject.name);
            }
        }
        else
        {
            Debug.LogError("Player1ScoreObject is not assigned in the inspector");
        }

        if (player2ScoreObject != null)
        {
            player2ScoreText = player2ScoreObject.GetComponent<Text>();
            if (player2ScoreText == null)
            {
                Debug.LogError("Player2ScoreText component is missing on " + player2ScoreObject.name);
            }
        }
        else
        {
            Debug.LogError("Player2ScoreObject is not assigned in the inspector");
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
