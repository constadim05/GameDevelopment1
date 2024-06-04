[System.Serializable]
public class GameData
{
    public string[] playerNames;
    public int[] kills;
    public int[] deaths;
    public string[] lastPlayerNames;
    public int maxKills;
    public float maxRoundTime;

    public GameData()
    {
        playerNames = new string[10];
        kills = new int[10];
        deaths = new int[10];
        lastPlayerNames = new string[2];
        maxKills = 10;
        maxRoundTime = 5.0f;
    }
}
