using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    //hold reference to current players
    [HideInInspector] public PlayerData currentPlayer1;
    [HideInInspector] public PlayerData currentPlayer2;

    //hold a temp list of scores to be sorted for highscores
    public List<PlayerData> tempPlayers = new List<PlayerData>(10);

    //debug switches
    public bool debugButtons;
    public bool loadOnStart = true;

    // Event for kill updates
    public delegate void KillUpdate();
    public event KillUpdate OnKillUpdate;

    //edit current players data like score and name
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

    //create a temp list of all players, filled in with data from saveData
    public void CreateTempList()
    {
        //generate a blank list
        tempPlayers = new List<PlayerData>();

        //get the players form saveData and put them in the list
        for (int i = 0; i < saveData.playerNames.Length; i++)
        {
            //create a player profile
            PlayerData newPlayer = new PlayerData();

            //input the information from the savedata to the new player
            newPlayer.playerName = saveData.playerNames[i];
            newPlayer.kills = saveData.kills[i];
            newPlayer.death = saveData.deaths[i];

            //calculate the kdr and input it
            if (newPlayer.death == 0) newPlayer.kdr = newPlayer.kills;
            else if (newPlayer.kills == 0) newPlayer.kdr = -newPlayer.death;
            else newPlayer.kdr = (float)newPlayer.kills / (float)newPlayer.death;

            //Add new player to list
            tempPlayers.Add(newPlayer);
        }
    }

    // Call this method when a player gets a kill
    public void PlayerGotKill(PlayerData player)
    {
        player.kills++;
        player.UpdatePlayerNameWithKills();

        // Trigger the event
        if (OnKillUpdate != null)
        {
            OnKillUpdate.Invoke();
        }

        // Save game after updating kills
        SaveGame();
    }

    //add our current players to the list
    //sort the list from highest to lowest scores
    public List<PlayerData> SortTempList(List<PlayerData> unSortedPlayers, bool addCurrentPlayers = false)
    {
        if (addCurrentPlayers)
        {
            //check if list already contains player 1
            if (tempPlayers.Find(p => p.playerName.Split(':')[0] == currentPlayer1.playerName.Split(':')[0]) == null)
            {
                tempPlayers.Add(currentPlayer1);
            }
            else //if the player already exists, then replace its score with your current score
            {
                PlayerData existingPlayer = tempPlayers.Find(p => p.playerName.Split(':')[0] == currentPlayer1.playerName.Split(':')[0]);
                existingPlayer.kills = currentPlayer1.kills;
                existingPlayer.death = currentPlayer1.death;

                //calculate the kdr and input it
                if (existingPlayer.death == 0) existingPlayer.kdr = existingPlayer.kills;
                else if (existingPlayer.kills == 0) existingPlayer.kdr = -existingPlayer.death;
                else existingPlayer.kdr = (float)existingPlayer.kills / (float)existingPlayer.death;
            }

            //check if list already contains player 2
            if (tempPlayers.Find(p => p.playerName.Split(':')[0] == currentPlayer2.playerName.Split(':')[0]) == null)
            {
                tempPlayers.Add(currentPlayer2);
            }
            else //if the player already exists, then replace its score with your current score
            {
                PlayerData existingPlayer = tempPlayers.Find(p => p.playerName.Split(':')[0] == currentPlayer2.playerName.Split(':')[0]);
                existingPlayer.kills = currentPlayer2.kills;
                existingPlayer.death = currentPlayer2.death;

                //calculate the kdr and input it
                if (existingPlayer.death == 0) existingPlayer.kdr = existingPlayer.kills;
                else if (existingPlayer.kills == 0) existingPlayer.kdr = -existingPlayer.death;
                else existingPlayer.kdr = (float)existingPlayer.kills / (float)existingPlayer.death;
            }
        }

        //sort the list by score
        List<PlayerData> sortedList = unSortedPlayers.OrderByDescending(o => o.kills).ToList();

        //remove any extras
        if (sortedList.Count > 10)
        {
            sortedList.RemoveRange(10, sortedList.Count - 10);
        }

        //save the list to the savedata
        for (int i = 0; i < sortedList.Count; i++)
        {
            //put the playername in saveData
            saveData.playerNames[i] = sortedList[i].playerName;
            saveData.kills[i] = sortedList[i].kills;
            saveData.deaths[i] = sortedList[i].death;
        }

        // Save the game after sorting
        SaveGame();

        return sortedList;
    }

    public void SaveGame()
    {
        // Save the game data using SaveSystem
        SaveSystem.SaveGame(saveData);
    }

    public void LoadGame()
    {
        // Load the game data using SaveSystem
        saveData = SaveSystem.LoadGame();

        // Make sure the loaded data isn't null
        if (saveData == null)
        {
            saveData = new GameData();
        }

        // Populate the player list
        CreateTempList();
    }
}
