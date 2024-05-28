using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject endgameText;
    private int playersAlive;

    void Start()
    {
        // Set the initial count of players alive
        playersAlive = GameObject.FindGameObjectsWithTag("Player").Length;

        // Ensure endgameText is initially inactive
        endgameText.SetActive(false);
    }

    public void PlayerDied()
    {
        playersAlive--;
        if (playersAlive <= 0)
        {
            ShowEndGameText();
        }
    }

    private void ShowEndGameText()
    {
        // Activate the endGameText GameObject
        endgameText.SetActive(true);

        // If the endGameText GameObject has an Animator component, trigger the animation
        Animator endGameAnim = endgameText.GetComponent<Animator>();
        if (endGameAnim != null)
        {
            endGameAnim.SetTrigger("endGameText");
        }
    }
}