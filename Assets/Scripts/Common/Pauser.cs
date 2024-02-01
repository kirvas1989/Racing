using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour
{
    public event UnityAction<bool> PauseStateChange;

    private bool isPause;
    public bool IsPause => isPause;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneManager_sceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneManager_sceneLoaded;
    }

    private void OnSceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UnPause();
    }

    public void ChangePauseState()
    {
        if (isPause == true)
            UnPause();
        else
            Pause();
    }

    public void Pause()
    {
        if (isPause == true) return;
        
        Time.timeScale = 0;
        isPause = true;
        PauseStateChange?.Invoke(isPause);
    }

    public void UnPause()
    {
        if (isPause == false) return;

        Time.timeScale = 1;
        isPause = false;
        PauseStateChange?.Invoke(isPause);
    }
}
