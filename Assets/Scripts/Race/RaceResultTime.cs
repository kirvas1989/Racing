using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RaceResultTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
{
    public static string SaveMark = "_player_best_time";

    public event UnityAction ResultUpdated;
    
    [SerializeField] private float goldTime;
    [SerializeField] private float silverTime;
    [SerializeField] private float bronzeTime;

    private float playerRecordTime;
    private float currentTime;

    public float GoldTime => goldTime;
    public float SilverTime => silverTime;
    public float BronzeTime => bronzeTime;
    public float PlayerRecordTime => playerRecordTime; 
    public float CurrentTime => currentTime;
    public bool RecordWasSet => playerRecordTime != 0;

    private RaceTimeTracker raceTimeTracker;
    public void Construct(RaceTimeTracker raceTimeTracker) => this.raceTimeTracker = raceTimeTracker;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker raceStateTracker) => this.raceStateTracker = raceStateTracker;

    private void Awake()
    {
        Load();
    }

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
        float absoluteRecord = GetAbsoluteRecord();
        
        if (raceTimeTracker.CurrentTime < absoluteRecord || playerRecordTime == 0)
        {
            playerRecordTime = raceTimeTracker.CurrentTime;

            Save();
        }

        currentTime = raceTimeTracker.CurrentTime;

        ResultUpdated?.Invoke();
    }

    public float GetAbsoluteRecord()
    {
        if (playerRecordTime < goldTime && playerRecordTime != 0)
            return playerRecordTime;
        else 
            return goldTime;
    }

    private void Load()
    {
        playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
    }
}
