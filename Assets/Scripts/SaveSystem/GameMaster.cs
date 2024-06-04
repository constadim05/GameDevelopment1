using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameData saveData;

    #region Singleton
    public static GameMaster instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [HideInInspector] public PlayerData currentPlayer1;
    [HideInInspector] public PlayerData currentPlayer2;

    public List<PlayerData> tempPlayers = new List<PlayerData>(10);

    public bool debugButtons;
    public bool loadOnStart = true;

    public void Start()
    {
        if (loadOnStart)
        {
            LoadGame();
        }
        else
        {
            saveData = new GameData();
            CreateTempList();
        }
    }

    public void CreateTempList()
    {
        tempPlayers = new List<PlayerData>();

        for (int i = 0; i < saveData.playerNames.Length; i++)
        {
            PlayerData newPlayer = new PlayerData
            {
                playerName = saveData.playerNames[i],
                kills = saveData.kills[i],
                death = saveData.deaths[i]
            };

            newPlayer.kdr = newPlayer.death == 0 ? newPlayer.kills : newPlayer.kills == 0 ? -newPlayer.death : (float)newPlayer.kills / newPlayer.death;

            tempPlayers.Add(newPlayer);
        }
    }

    public List<PlayerData> SortTempList(List<PlayerData> unSortedPlayers, bool addCurrentPlayers = false)
    {
        if (addCurrentPlayers)
        {
            UpdatePlayerData(currentPlayer1);
            UpdatePlayerData(currentPlayer2);
        }

        return unSortedPlayers.OrderByDescending(p => p.kdr).ToList();
    }

    private void UpdatePlayerData(PlayerData player)
    {
        PlayerData existingPlayer = tempPlayers.Find(p => p.playerName == player.playerName);
        if (existingPlayer == null)
        {
            tempPlayers.Add(player);
        }
        else
        {
            existingPlayer.kills = player.kills;
            existingPlayer.death = player.death;
            existingPlayer.kdr = existingPlayer.death == 0 ? existingPlayer.kills : existingPlayer.kills == 0 ? -existingPlayer.death : (float)existingPlayer.kills / existingPlayer.death;
        }
    }

    public void SendHighScoresToSaveData(List<PlayerData> players)
    {
        for (int i = 0; i < 10; i++)
        {
            saveData.playerNames[i] = players[i].playerName;
            saveData.kills[i] = players[i].kills;
            saveData.deaths[i] = players[i].death;
        }
    }

    public void SaveGame()
    {
        tempPlayers = SortTempList(tempPlayers, false);
        SendHighScoresToSaveData(tempPlayers);

        saveData.lastPlayerNames[0] = currentPlayer1.playerName;
        saveData.lastPlayerNames[1] = currentPlayer2.playerName;

        SaveSystem.instance.SaveGame(saveData);
    }

    public void LoadGame()
    {
        saveData = SaveSystem.instance.LoadGame() ?? new GameData();
        currentPlayer1.playerName = saveData.lastPlayerNames[0];
        currentPlayer2.playerName = saveData.lastPlayerNames[1];
        CreateTempList();
    }

    #region Debugging
    private void Update()
    {
        if (!debugButtons) return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            tempPlayers = SortTempList(tempPlayers, false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomFillData();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ClearData();
        }
    }

    private void ClearData()
    {
        foreach (PlayerData player in tempPlayers)
        {
            player.playerName = "";
            player.kills = 0;
            player.death = 0;
            player.kdr = 0;
        }
    }

    private void RandomFillData()
    {
        string glyphs = "abcdefghijklmnopqrstuvwxyz";

        foreach (PlayerData player in tempPlayers)
        {
            player.playerName = new string(Enumerable.Range(0, Random.Range(3, 10)).Select(_ => glyphs[Random.Range(0, glyphs.Length)]).ToArray());
            player.kills = Random.Range(0, 20);
            player.death = Random.Range(0, 20);
            player.kdr = player.death == 0 ? player.kills : player.kills == 0 ? -player.death : (float)player.kills / player.death;
        }
    }
    #endregion
}
