using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MainMenuUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button changeNameButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button scoresButton;
    [SerializeField] private Button exitButton;
    private void Start()
    {


        GameManager.Instance.PlayerName = PlayerPrefs.GetString("PlayerName", "Jugador");
        UpdatePlayerNameDisplay();

 
        playButton.onClick.AddListener(OnPlayButtonClicked);
        scoresButton.onClick.AddListener(OnScoresButtonClicked);
        changeNameButton.onClick.AddListener(ChangePlayerName);

       
        nameInputField.shouldHideMobileInput = false;
    }



    private void SetButtonSize(Button button, int width, int height)
    {
        if (button != null)
        {
            RectTransform rect = button.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(width, height);

       
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.fontSize = 28;
                buttonText.margin = new Vector4(10, 10, 10, 10);
            }
        }
    }

    public void ChangePlayerName()
    {
        string newName = nameInputField.text.Trim();

        if (!string.IsNullOrEmpty(newName))
        {
            GameManager.Instance.PlayerName = newName;
            PlayerPrefs.SetString("PlayerName", newName);
            PlayerPrefs.Save();

            UpdatePlayerNameDisplay();
            nameInputField.text = "";

            // Ocultar teclado virtual en móvil
#if UNITY_ANDROID || UNITY_IOS
            TouchScreenKeyboard.hideInput = true;
#endif
        }
    }

    private void UpdatePlayerNameDisplay()
    {
        playerNameText.text = $"Jugador: {GameManager.Instance.PlayerName}";
    }

    private void OnPlayButtonClicked()
    {
        GameManager.Instance.StartGame();
    }

    private void OnScoresButtonClicked()
    {
        GameManager.Instance.ShowScores();
    }
}

