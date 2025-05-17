using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoresUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Transform scoresContainer;
    [SerializeField] private GameObject scoreEntryPrefab;
    [SerializeField] private Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        LoadScores();
    }

    private void LoadScores()
    {
       
        foreach (Transform child in scoresContainer)
        {
            Destroy(child.gameObject);
        }

        
        FirebaseManager.Instance.GetTopScores(5, (scores) => {
            for (int i = 0; i < scores.Count; i++)
            {
                var entry = Instantiate(scoreEntryPrefab, scoresContainer);
                var texts = entry.GetComponentsInChildren<TMP_Text>();
                texts[0].text = $"{i + 1}. {scores[i].playerName}";
                texts[1].text = scores[i].score.ToString();
            }
        });
    }

    private void OnBackButtonClicked()
    {
        GameManager.Instance.ReturnToMenu();
    }
}