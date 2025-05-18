using Firebase.Database;
using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    private DatabaseReference _databaseReference;

    void Start()
    {
        // Inicialización básica de Firebase
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveScore(ScoreData scoreData)
    {
        DatabaseReference newEntry = _databaseReference.Child("scores").Push();
        newEntry.SetValueAsync(JsonUtility.ToJson(scoreData));
    }
}