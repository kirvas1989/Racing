using UnityEngine;
using UnityEngine.UI;

public class UICountdownTimer : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Pauser>
{
    [SerializeField] private Text text;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker raceStateTracker) => this.raceStateTracker = raceStateTracker;

    private Pauser pauser;
    public void Construct(Pauser pauser) => this.pauser = pauser;

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Started += OnRaceStarted;

        pauser.PauseStateChange += OnPauseStateChanged;

        text.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Started -= OnRaceStarted;

        pauser.PauseStateChange -= OnPauseStateChanged;
    }

    private void OnPreparationStarted()
    {
        text.enabled = true;
        enabled = true;
    }

    private void OnPauseStateChanged(bool pause)
    {
        if (raceStateTracker.State == RaceState.CountDown)
        {
            if (pause == true)
                text.gameObject.SetActive(false);

            if (pause == false)
                text.gameObject.SetActive(true);
        }
        else return;       
    }

    private void OnRaceStarted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void Update()
    {
        text.text = raceStateTracker.CountdownTimer.Value.ToString("F0");

        if (text.text == "0")
            text.text = "GO";
    }
}
