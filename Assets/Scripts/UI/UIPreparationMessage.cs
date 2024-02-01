using UnityEngine;

public class UI : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Pauser>
{
    [SerializeField] private GameObject preparationMessageObject;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker raceStateTracker) => this.raceStateTracker = raceStateTracker;

    private Pauser pauser;
    public void Construct(Pauser pauser) => this.pauser = pauser;

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        pauser.PauseStateChange += OnPauseStateChanged;

        preparationMessageObject.SetActive(true);
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        pauser.PauseStateChange -= OnPauseStateChanged;
    }

    private void OnPauseStateChanged(bool pause)
    {
        if (raceStateTracker.State == RaceState.Preparation)
        {
            if (pause == true)
                preparationMessageObject.SetActive(false);

            if (pause == false)
                preparationMessageObject.SetActive(true);
        }
        else return;      
    }

    private void OnPreparationStarted()
    {
        preparationMessageObject.SetActive(false);
    }
}
