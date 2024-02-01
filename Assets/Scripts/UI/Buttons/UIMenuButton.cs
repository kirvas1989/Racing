using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuButton : MonoBehaviour
{
    private const string GameOpenScene = "start_screen";

    public void GoToMenu()
    {
        SceneManager.LoadScene(GameOpenScene);
    }
}
