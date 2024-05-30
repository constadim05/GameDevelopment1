using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;

    public GameObject player1ScoreObject;
    public GameObject player2ScoreObject;

    private TextMesh player1ScoreText;
    private TextMesh player2ScoreText;

    void Start()
    {
        // Get the TextMesh components from the GameObjects
        if (player1ScoreObject != null)
        {
            player1ScoreText = player1ScoreObject.GetComponent<TextMesh>();
        }

        if (player2ScoreObject != null)
        {
            player2ScoreText = player2ScoreObject.GetComponent<TextMesh>();
        }

        UpdateScoreUI();
    }

    public void IncreasePlayerScore(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1Score++;
        }
        else if (playerNumber == 2)
        {
            player2Score++;
        }

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (player1ScoreText != null)
        {
            player1ScoreText.text = "Player 1: " + player1Score;
        }

        if (player2ScoreText != null)
        {
            player2ScoreText.text = "Player 2: " + player2Score;
        }
    }
}
