using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/*
public class ScoresUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform scoresContainer;
    [SerializeField] private GameObject scoreEntryPrefab;
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_Text loadingText;

    private void Start()
    {
        // Configurar bot�n de regreso
        backButton.onClick.AddListener(() => GameManager.Instance.ReturnToMenu());

        // Cargar y mostrar puntuaciones
        LoadLocalScores();
    }

    private void LoadLocalScores()
    {
        // Limpiar contenedor
        foreach (Transform child in scoresContainer)
        {
            Destroy(child.gameObject);
        }

        // Mostrar puntuaci�n actual del jugador
        CreateScoreEntry(1, GameManager.Instance.PlayerName, GameManager.Instance.CurredScore);

        // Mostrar r�cord personal
        CreateScoreEntry(2, GameManager.Instance.PlayerName + " (R�cord)", GameManager.Instance.HighScore);
    }

    private void CreateScoreEntry(int position, string playerName, int score)
    {
        if (scoreEntryPrefab == null || scoresContainer == null) return;

        var entry = Instantiate(scoreEntryPrefab, scoresContainer);
        var texts = entry.GetComponentsInChildren<TMP_Text>();

        if (texts.Length >= 2)
        {
            texts[0].text = $"{position}. {playerName}";
            texts[1].text = score.ToString();
        }
    }
}
*/