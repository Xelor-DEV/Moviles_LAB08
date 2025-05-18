using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ScoreDataSO", menuName = "Scriptable Objects/ScoreDataSO")]
public class ScoreDataSO : ScriptableObject
{
    public string userId;
    public string username;
    public int currentScore;

    public ScoreData ToScoreData()
    {
        return new ScoreData
        {
            userId = this.userId,
            username = this.username,
            score = this.currentScore
        };
    }
}

[Serializable]
public class ScoreData
{
    public string userId;
    public string username;
    public int score;
}