using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public TMP_InputField player1Name;
    public TMP_InputField player2Name;
    public TMP_InputField maxKills;
    public TMP_InputField maxTime;

    public Button playButton;
    public Button player1ReadyUp;
    public Button player2ReadyUp;
    public Button player1NotReady;
    public Button player2NotReady;

    public bool player1IsReady;
    public bool player2IsReady;

    void Start()
    {
        player1Name.text = string.IsNullOrEmpty(GameMaster.instance.saveData.lastPlayerNames[0]) ? "Insert Player Name" : GameMaster.instance.saveData.lastPlayerNames[0];
        player2Name.text = string.IsNullOrEmpty(GameMaster.instance.saveData.lastPlayerNames[1]) ? "Insert Player Name" : GameMaster.instance.saveData.lastPlayerNames[1];
        maxKills.text = GameMaster.instance.saveData.maxKills.ToString();
        maxTime.text = GameMaster.instance.saveData.maxRoundTime.ToString();
    }

    public void UpdatePlayerName(int playerNum)
    {
        if (playerNum == 1)
        {
            GameMaster.instance.currentPlayer1.playerName = player1Name.text;
        }
        else if (playerNum == 2)
        {
            GameMaster.instance.currentPlayer2.playerName = player2Name.text;
        }
    }

    public void UpdateKills()
    {
        GameMaster.instance.saveData.maxKills = int.Parse(maxKills.text);
    }

    public void UpdateTime()
    {
        GameMaster.instance.saveData.maxRoundTime = float.Parse(maxTime.text);
    }

    public void EnableBools(int playerNum)
    {
        if (playerNum == 1)
        {
            player1IsReady = true;
            player1ReadyUp.interactable = false;
            player1NotReady.interactable = true;
        }
        else if (playerNum == 2)
        {
            player2IsReady = true;
            player2ReadyUp.interactable = false;
            player2NotReady.interactable = true;
        }
        playButton.interactable = player1IsReady && player2IsReady;
    }

    public void DisableBools(int playerNum)
    {
        if (playerNum == 1)
        {
            player1IsReady = false;
            player1ReadyUp.interactable = true;
            player1NotReady.interactable = false;
        }
        else if (playerNum == 2)
        {
            player2IsReady = false;
            player2ReadyUp.interactable = true;
            player2NotReady.interactable = false;
        }
        playButton.interactable = player1IsReady && player2IsReady;
    }
}
