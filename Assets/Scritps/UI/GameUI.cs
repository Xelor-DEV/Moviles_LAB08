using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private void Update()
    {
        scoreText.text = $"Puntaje: {GameManager.Instance.CurredScore}";
        highScoreText.text = $"Record: {GameManager.Instance.HighScore}";
    }
}
