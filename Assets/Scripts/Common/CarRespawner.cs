using UnityEngine;

public class CarRespawner : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Car>, IDependency<CarInputControl>
{
    [SerializeField] private float respawnHeight;
    
    private TrackPoint respawnTrackPoint;
    
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker raceStateTracker) => this.raceStateTracker = raceStateTracker;

    private Car car;
    public void Construct(Car car) => this.car = car;

    private CarInputControl carInputControl;
    public void Construct(CarInputControl carInputControl) => this.carInputControl = carInputControl;

    private void Start()
    {
        raceStateTracker.TrackPointPassed += OnTrackPointPassed;
    }

    private void OnDestroy()
    {
        raceStateTracker.TrackPointPassed -= OnTrackPointPassed;
    }

    private void OnTrackPointPassed(TrackPoint trackPoint)
    {
        respawnTrackPoint = trackPoint;
    }

    public void Respawn()
    {
        if (respawnTrackPoint == null) return;

        if (raceStateTracker.State != RaceState.Race) return;

        car.Respawn(respawnTrackPoint.transform.position + respawnTrackPoint.transform.up * respawnHeight, respawnTrackPoint.transform.rotation);

        car.transform.Rotate(0, -90, 0); 

        carInputControl.Reset();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) == true)
        {
            Respawn();
        }           
    }
}
