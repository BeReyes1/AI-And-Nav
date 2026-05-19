using UnityEngine;
using TMPro;

public class ConditionManager : MonoBehaviour
{

    public static ConditionManager Instance;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TextMeshProUGUI resultText; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Time.timeScale = 1f;
    }

    public void GameOver(bool won)
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        resultText.text = won ? "YOU WIN!" : "YOU LOST";
        gameOverCanvas.SetActive(true);
    }

}
