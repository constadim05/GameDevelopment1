[System.Serializable]
public class GameData
{
    public string[] playerNames;
    public int[] kills;
    public int[] deaths;
    public string[] lastPlayerNames;
    public int maxKills;
    public float maxRoundTime;

    public int score = 0;
    public string player1Name;

    public void AddScore(int points)
    {
        score += points;
    }
    public void ResetData()
    {
        score = 0;
    }
    
}
