using UnityEngine;
using UnityEngine.UI;

public class UIRaceProgress : MonoBehaviour, IDependency<TrackpointCircuit>, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject progressPanel;
    [SerializeField] private Text title;
    [SerializeField] private Text progressValue;

    private int gateAmount;

    private TrackpointCircuit trackpointCircuit;
    public void Construct(TrackpointCircuit trackpointCircuit) => this.trackpointCircuit = trackpointCircuit;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker raceStateTracker) => this.raceStateTracker = raceStateTracker;

    private void Start()
    {
        progressPanel.SetActive(false);

        if (trackpointCircuit.Type == TrackType.Circular)
        {
            title.text = "LAPS:";
            progressValue.text = raceStateTracker.LapsToComplete.ToString();
        }

        if (trackpointCircuit.Type == TrackType.Sprint)
        {
            title.text = "GATES:";
            gateAmount = trackpointCircuit.Points;
            progressValue.text = gateAmount.ToString();
        }

        trackpointCircuit.TrackPointTriggered += OnTrackPointTriggered;

        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.LapCompleted += OnLapCompleted;
        raceStateTracker.Completed += OnRaceCompleted;
    }

    private void OnDestroy()
    {
        trackpointCircuit.TrackPointTriggered -= OnTrackPointTriggered;

        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.LapCompleted -= OnLapCompleted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceStarted()
    {
        progressPanel.SetActive(true);
    }

    private void OnTrackPointTriggered(TrackPoint point)
    {
        if (trackpointCircuit.Type == TrackType.Sprint)
        {
            gateAmount = --gateAmount;
            progressValue.text = gateAmount.ToString();
        }
    }

    private void OnLapCompleted(int lapAmount)
    {
        if (trackpointCircuit.Type == TrackType.Circular)
            progressValue.text = (raceStateTracker.LapsToComplete - lapAmount).ToString();
    }

    private void OnRaceCompleted()
    {
        progressPanel.SetActive(false);
    }
}
