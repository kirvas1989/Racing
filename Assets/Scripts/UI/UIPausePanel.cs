using UnityEngine;

public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
{
    [SerializeField] private GameObject pausePanel;
    
    private Pauser pauser;
    public void Construct(Pauser pauser) => this.pauser = pauser;

    private void Start()
    {
        pausePanel.SetActive(false);
        pauser.PauseStateChange += OnPauseStateChange;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStateChange;
    }

    private void OnPauseStateChange(bool pause)
    {
        pausePanel.SetActive(pause);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            pauser.ChangePauseState();
        }
    }

    public void UnPause()
    {
        pauser.UnPause();
    }
}
