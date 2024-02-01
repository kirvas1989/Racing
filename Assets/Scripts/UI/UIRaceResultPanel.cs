using UnityEngine;
using UnityEngine.UI;

public class UIRaceResultPanel : MonoBehaviour, IDependency<RaceResultTime>, IDependency<Pauser>, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject resultPanelObject;
    [SerializeField] private Text recordTime;
    [SerializeField] private Text currentTime;
    
    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime raceResultTime) => this.raceResultTime = raceResultTime;

    private Pauser pauser;
    public void Construct(Pauser pauser) => this.pauser = pauser;
    
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker raceStateTracker) => this.raceStateTracker = raceStateTracker;

    private void Start()
    {
        resultPanelObject.SetActive(false);
        
        raceResultTime.ResultUpdated += OnUpdateResults;
        pauser.PauseStateChange += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        raceResultTime.ResultUpdated -= OnUpdateResults;
        pauser.PauseStateChange -= OnPauseStateChanged;
    }

    private void OnUpdateResults()
    {
        resultPanelObject.SetActive(true);

        recordTime.text = StringTime.SecondToTimeString(raceResultTime.GetAbsoluteRecord());
        currentTime.text = StringTime.SecondToTimeString(raceResultTime.CurrentTime);
    }

    private void OnPauseStateChanged(bool pause)
    {
        if (raceStateTracker.State == RaceState.Passed)
        {
            if (pause == true)
                resultPanelObject.SetActive(false);

            if (pause == false)
                resultPanelObject.SetActive(true);
        }
        else return;
    }
}
