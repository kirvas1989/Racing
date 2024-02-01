using UnityEngine;

public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker> 
{
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker raceStateTracker) => this.raceStateTracker = raceStateTracker;

    private float currentTime;
    public float CurrentTime => currentTime;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceStarted()
    {
        enabled = true;
        currentTime = 0;
    }

    private void OnRaceCompleted()
    {
        enabled = false;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }
}
