[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int kills;
    public int death;
    public float kdr;

    public void UpdatePlayerNameWithKills()
    {
        playerName = $"{playerName.Split(':')[0]}: {kills}";
    }
}
