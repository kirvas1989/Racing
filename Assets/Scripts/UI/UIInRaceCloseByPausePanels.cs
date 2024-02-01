using UnityEngine;

public class UIInRaceCloseByPausePanels : MonoBehaviour, IDependency<Pauser>
{
    [SerializeField] private GameObject[] settingsPanelObjects;

    private Pauser pauser;
    public void Construct(Pauser pauser) => this.pauser = pauser;

    private void Start()
    {
        for (int i = 0; i < settingsPanelObjects.Length; i++)
        {
            settingsPanelObjects[i].SetActive(false);
        }
        
        pauser.PauseStateChange += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStateChanged;
    }

    private void OnPauseStateChanged(bool pause)
    {
        for (int i = 0; i < settingsPanelObjects.Length; i++)
        {
            if (pause == false)
                settingsPanelObjects[i].SetActive(false);
            else return;
        }
    }
}
