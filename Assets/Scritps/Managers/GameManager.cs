using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

    [Header("Player Info")]
    public string PlayerName { get; set; } = "Jugador";
    public int CurredScore = 0;
    public int HighScore =0;

    [Header("Game Settings")]
    public float gameSpeed =1.0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPlayerData();

        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartGame()
    {
        CurredScore = 0;
        Time.timeScale = gameSpeed;
        SceneManager.LoadScene("Game");
    }
    public void ShowScores()
    {
        SceneManager.LoadScene("Scores");
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void UpdateScore(int score)
    {
        CurredScore += score;
         if(CurredScore > HighScore)
         {
            HighScore = CurredScore;
            SavePlayerData();

        }
    }
    public bool IsNewRecord()
    {
        return CurredScore > HighScore;
    }
    public void GameOver()
    {
        //FirebaseManager.Instance.SaveScore(playerName,currentScore, IsNewRecord());
        SceneManager.LoadScene("GameOver");

    }
    private void LoadPlayerData()
    {
        PlayerName = PlayerPrefs.GetString("PlayerName", "Jugador");
        HighScore = PlayerPrefs.GetInt("HighScore", 0);

    }
    private void SavePlayerData()
    {
        PlayerPrefs.SetString("PlayerName", PlayerName);
        PlayerPrefs.SetInt("HighScore", HighScore);
        PlayerPrefs.Save();
    }
}
