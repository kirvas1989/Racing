using UnityEngine;
using UnityEngine.UI;

public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceResultTime>, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject playerRecordObject;
    [SerializeField] private GameObject goldRecordObject;
    [SerializeField] private GameObject silverRecordObject;
    [SerializeField] private GameObject bronzeRecordObject;

    [SerializeField] private Text playerRecordTime;
    [SerializeField] private Text goldRecordTime;
    [SerializeField] private Text silverRecordTime;
    [SerializeField] private Text bronzeRecordTime;

    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime raceResultTime) => this.raceResultTime = raceResultTime;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker raceStateTracker) => this.raceStateTracker = raceStateTracker;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        playerRecordObject.SetActive(false);
        goldRecordObject.SetActive(false);
        silverRecordObject.SetActive(false);
        bronzeRecordObject.SetActive(false);
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceStarted()
    {
        if (raceResultTime.PlayerRecordTime > raceResultTime.BronzeTime || raceResultTime.RecordWasSet == false)
        {
            bronzeRecordObject.SetActive(true);
            bronzeRecordTime.text = StringTime.SecondToTimeString(raceResultTime.BronzeTime);
        }

        if (raceResultTime.PlayerRecordTime > raceResultTime.SilverTime || raceResultTime.RecordWasSet == false)
        {
            silverRecordObject.SetActive(true);
            silverRecordTime.text = StringTime.SecondToTimeString(raceResultTime.SilverTime);
        }

        if (raceResultTime.PlayerRecordTime > raceResultTime.GoldTime || raceResultTime.RecordWasSet == false)
        {
            goldRecordObject.SetActive(true);
            goldRecordTime.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
        }

        if (raceResultTime.RecordWasSet == true)
        {
            playerRecordObject.SetActive(true);
            playerRecordTime.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
        }
    }

    private void OnRaceCompleted()
    {
        playerRecordObject.SetActive(false);
        goldRecordObject.SetActive(false);
        silverRecordObject.SetActive(false);
        bronzeRecordObject.SetActive(false);
    }
}
