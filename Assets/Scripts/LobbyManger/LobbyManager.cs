using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    // UI Elements
    public TMP_InputField player1Name;
    public TMP_InputField player2Name;
    public TMP_InputField maxKills;
    public TMP_InputField maxTime;

    // UI buttons
    public Button playButton;
    public Button player1ReadyUp;
    public Button player2ReadyUp;
    public Button player1NotReady;
    public Button player2NotReady;

    // bools
    public bool player1IsReady;
    public bool player2IsReady;

    PlayerScoreManager playerScoreManager; // Reference to PlayerScoreManager

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

        // Load player names from saveData
        if (GameMaster.instance.saveData.lastPlayerNames[0] == "")
        {
            player1Name.text = "Insert Player Name";
        }
        else
        {
            player1Name.text = GameMaster.instance.saveData.lastPlayerNames[0];
        }
        if (GameMaster.instance.saveData.lastPlayerNames[1] == "")
        {
            player2Name.text = "Insert Player Name";
        }
        else
        {
            player2Name.text = GameMaster.instance.saveData.lastPlayerNames[1];
        }
        maxKills.text = GameMaster.instance.saveData.maxKills.ToString();
        maxTime.text = GameMaster.instance.saveData.maxRoundTime.ToString();

        // Subscribe to kill updates
        GameMaster.instance.OnKillUpdate += UpdatePlayerNamesWithKills;
    }

    private void OnDestroy()
    {
        // Unsubscribe from kill updates
        GameMaster.instance.OnKillUpdate -= UpdatePlayerNamesWithKills;
    }

    private void UpdatePlayerNamesWithKills()
    {
        player1Name.text = GameMaster.instance.currentPlayer1.playerName;
        player2Name.text = GameMaster.instance.currentPlayer2.playerName;
    }

    public void UpdatePlayerName(int playerNum)
    {
        if (playerNum == 1)
        {
            GameMaster.instance.currentPlayer1.playerName = player1Name.text;
            GameMaster.instance.saveData.lastPlayerNames[0] = player1Name.text; // Save the name
        }
        if (playerNum == 2)
        {
            GameMaster.instance.currentPlayer2.playerName = player2Name.text;
            GameMaster.instance.saveData.lastPlayerNames[1] = player2Name.text; // Save the name
        }
        GameMaster.instance.SaveGame(); // Save the game data
    }

    public void UpdateKills()
    {
        GameMaster.instance.saveData.maxKills = int.Parse(maxKills.text);
        // Update max kills text in PlayerScoreManager
        if (playerScoreManager != null)
        {
            playerScoreManager.UpdateMaxKillsText(GameMaster.instance.saveData.maxKills);
        }
        GameMaster.instance.SaveGame(); // Save the game data
    }

    public void UpdateTime()
    {
        GameMaster.instance.saveData.maxRoundTime = float.Parse(maxTime.text);
        GameMaster.instance.SaveGame(); // Save the game data
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
