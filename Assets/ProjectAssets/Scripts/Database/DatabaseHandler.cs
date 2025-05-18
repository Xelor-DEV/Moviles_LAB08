using Firebase.Database;
using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    private DatabaseReference _databaseReference;

    void Start()
    {
        // Inicializaci�n b�sica de Firebase
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveScore(ScoreData scoreData)
    {
        DatabaseReference newEntry = _databaseReference.Child("scores").Push();
        newEntry.SetValueAsync(JsonUtility.ToJson(scoreData));
    }
}