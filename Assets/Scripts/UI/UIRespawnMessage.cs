using UnityEngine;

public class UIRespawnMessage : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject respawnPanel;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker raceStateTracker) => this.raceStateTracker = raceStateTracker;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        respawnPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceStarted()
    {
        respawnPanel.SetActive(true);
    }

    private void OnRaceCompleted()
    {
        respawnPanel.SetActive(false);
    }
}
