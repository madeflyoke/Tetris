using UnityEngine;

public class PlayerPrefsInfo 
{
    private const string MaxScoreKey = "MaxScore";
    public int MaxScore { get; private set; }
    public int CurrentScore=0;

    public PlayerPrefsInfo()
    {
        MaxScore = PlayerPrefs.GetInt(MaxScoreKey);
    }
    public void SaveMaxScore()
    {
        if (MaxScore<CurrentScore)
        {
            MaxScore = CurrentScore;
            PlayerPrefs.SetInt(MaxScoreKey, MaxScore);
        }
    }
    public void ResetMaxScore()
    {
        PlayerPrefs.SetInt(MaxScoreKey, 0);
        MaxScore = 0;
    }


}
