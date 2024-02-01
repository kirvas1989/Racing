using UnityEngine;
using UnityEngine.UI;

public class UIRaceCompletedMessage : MonoBehaviour, IDependency<RaceResultTime>, IDependency<RaceStateTracker> 
{
    [SerializeField] private GameObject CompleteMessageObject;
    [SerializeField] private Text recordTimeText;
    [SerializeField] private Text currentTimeText;

    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime raceResultTime) => this.raceResultTime = raceResultTime;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker raceStateTracker) => this.raceStateTracker = raceStateTracker;

    private void Start()
    {
        raceStateTracker.Completed += OnRaceCompleted;

        CompleteMessageObject.SetActive(false);
    }

    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceCompleted()
    {
        CompleteMessageObject.SetActive(true);

        recordTimeText.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
        
        if (raceResultTime.RecordWasSet ==  false)
            currentTimeText.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
        else
            currentTimeText.text = StringTime.SecondToTimeString(raceResultTime.CurrentTime);
    }
}
