[System.Serializable]
public class GameData
{
    public string[] playerNames = new string[10];
    public int[] kills = new int[10];
    public int[] deaths = new int[10];
    public string[] lastPlayerNames = new string[2];

    public int maxKills;
    public float maxRoundTime;
}
