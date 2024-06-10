using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    //UI Elements
    public TMP_InputField player1Name;
    public TMP_InputField player2Name;
    public TMP_InputField maxKills;
    public TMP_InputField maxTime;

    //UI buttons
    public Button playButton;
    public Button player1ReadyUp;
    public Button player2ReadyUp;
    public Button player1NotReady;
    public Button player2NotReady;
    //bools
    public bool player1IsReady;
    public bool player2IsReady;

    PlayerScoreManager playerScoreManager; // Reference to PlayerScoreManager
    public Timer timer; // Reference to the Timer script

    // Start is called before the first frame update
    void Start()
    {
        // Initialize playerScoreManager reference
        playerScoreManager = FindObjectOfType<PlayerScoreManager>();

        // Check if playerScoreManager is null
        if (playerScoreManager == null)
        {
            Debug.LogError("PlayerScoreManager is not found in the scene");
        }
        else
        {
            // Update max kills text on start
            playerScoreManager.UpdateMaxKillsText(GameMaster.instance.saveData.maxKills);
        }

        if (GameMaster.instance.saveData.lastPlayerNames[0] == "")
        {
            player1Name.text = "Insert Player Name";
        }
        else
        {
            player1Name.text = GameMaster.instance.saveData.playerNames[0];
        }
        if (GameMaster.instance.saveData.lastPlayerNames[1] == "")
        {
            player2Name.text = "Insert Player Name";
        }
        else
        {
            player2Name.text = GameMaster.instance.saveData.playerNames[1];
        }
        maxKills.text = GameMaster.instance.saveData.maxKills.ToString();
        maxTime.text = GameMaster.instance.saveData.maxRoundTime.ToString();

        // Find and set reference to Timer script
        timer = FindObjectOfType<Timer>();
    }

    public void UpdatePlayerName(int playerNum)
    {
        if (playerNum == 1)
        {
            GameMaster.instance.currentPlayer1.playerName = player1Name.text;
        }
        if (playerNum == 2)
        {
            GameMaster.instance.currentPlayer2.playerName = player2Name.text;
        }
    }

    public void UpdateKills()
    {
        GameMaster.instance.saveData.maxKills = int.Parse(maxKills.text);
        // Update max kills text in PlayerScoreManager
        if (playerScoreManager != null)
        {
            playerScoreManager.UpdateMaxKillsText(GameMaster.instance.saveData.maxKills);
        }
    }

    public void UpdateTime()
    {
        float newMaxTime = float.Parse(maxTime.text);

        // Update max time in Timer script
        timer.maxTime = newMaxTime;

        GameMaster.instance.saveData.maxRoundTime = newMaxTime;
    }

    public void EnableBools(int playerNum)
    {
        if (playerNum == 1)
        {
            player1IsReady = true;
            player1ReadyUp.interactable = false;
            player1NotReady.interactable = true;
        }
        if (playerNum == 2)
        {
            player2IsReady = true;
            player2ReadyUp.interactable = false;
            player2NotReady.interactable = true;
        }
        if (player1IsReady && player2IsReady)
        {
            playButton.interactable = true;
        }
        else
        {
            playButton.interactable = false;
        }
    }

    public void DisableBools(int playerNum)
    {
        if (playerNum == 1)
        {
            player1IsReady = false;
            player1ReadyUp.interactable = true;
            player1NotReady.interactable = false;
        }
        if (playerNum == 2)
        {
            player2IsReady = false;
            player2ReadyUp.interactable = true;
            player2NotReady.interactable = false;
        }
        if (player1IsReady && player2IsReady)
        {
            playButton.interactable = true;
        }
        else
        {
            playButton.interactable = false;
        }
    }
}
