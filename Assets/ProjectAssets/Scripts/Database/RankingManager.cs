using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RankingManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private int defaultEntries = 5;

    [Header("Events")]
    public UnityEvent<List<ScoreData>> OnRankingUpdated = new UnityEvent<List<ScoreData>>();

    private DatabaseReference scoresReference;

    void Start()
    {
        InitializeFirebase();
        LoadTopScores(defaultEntries);
    }

    private void InitializeFirebase()
    {
        scoresReference = FirebaseDatabase.DefaultInstance.GetReference("scores");
    }

    public void LoadTopScores(int numberOfEntries)
    {
        StartCoroutine(LoadTopScoresCoroutine(numberOfEntries));
    }

    private IEnumerator LoadTopScoresCoroutine(int maxEntries)
    {
        var query = scoresReference.OrderByChild("score").LimitToLast(maxEntries);
        var task = query.GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task == null || task.IsFaulted)
        {
            Debug.LogError("Error al cargar scores");
            yield break;
        }

        ProcessSnapshot(task.Result);
    }

    private void ProcessSnapshot(DataSnapshot snapshot)
    {
        List<ScoreData> scores = new List<ScoreData>();

        foreach (DataSnapshot child in snapshot.Children)
        {
            ScoreData entry = JsonUtility.FromJson<ScoreData>(child.GetRawJsonValue());
            scores.Add(entry);
        }

        scores.Sort((a, b) => b.score.CompareTo(a.score));
        OnRankingUpdated?.Invoke(scores);
    }
}