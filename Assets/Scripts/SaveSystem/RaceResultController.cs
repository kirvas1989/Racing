using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceResultController : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResultTime>
{
    private bool isLocked;
       
    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime raceResultTime) => this.raceResultTime = raceResultTime;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker raceStateTracker) => this.raceStateTracker = raceStateTracker;

    private void Start()
    {
        raceStateTracker.Completed += OnRaceCompleted;
    }

    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceCompleted()
    {
        float playerRecordTime = raceResultTime.GetAbsoluteRecord();

        if (playerRecordTime < raceResultTime.BronzeTime)
            isLocked = false;

        RaceCompletion.SaveRaceResult(SceneManager.GetActiveScene().name, playerRecordTime, isLocked);
    }
}
