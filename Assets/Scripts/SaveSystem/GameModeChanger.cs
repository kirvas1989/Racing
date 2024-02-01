using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeChanger : MonoBehaviour
{
    [SerializeField] private GameObject cautionGamePanel;
    [SerializeField] private UIButton continueButton;

    private const string MainMenuScene = "main_menu";
    private const string StartScreenScene = "start_screen";

    private void Start()
    {
        if (continueButton != null)
            continueButton.gameObject.SetActive(FileHandler.HasFile(RaceCompletion.Filename));

        if (cautionGamePanel != null)
            cautionGamePanel.SetActive(false);
    }

    public void ShowCautionMessage()
    {
        if (FileHandler.HasFile(RaceCompletion.Filename))
            cautionGamePanel.SetActive(true);
        else
            StartNewGame();
    }

    public void ResetStatistics()
    {
        PlayerPrefs.DeleteAll();
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        FileHandler.Reset(RaceCompletion.Filename);
        SceneManager.LoadScene(MainMenuScene);
    }

    public void Continue()
    {
        SceneManager.LoadScene(MainMenuScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToStartScreen()
    {
        SceneManager.LoadScene(StartScreenScene);
    }
}
