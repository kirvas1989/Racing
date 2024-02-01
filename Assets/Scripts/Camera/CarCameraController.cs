using UnityEngine;

public class CarCameraController : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private Car car;
    [SerializeField] private new Camera camera;

    [Header("Components:")]
    [SerializeField] private CarCameraFollow follower;
    [SerializeField] private CarCameraFovCorrector fovCorrector;
    [SerializeField] private CarCameraShaker shaker;   
    [SerializeField] private CarCameraMotionBlur motionBlur;
    [SerializeField] private CarCameraVignette vignette;
    [SerializeField] private CameraPathFollower pathFollower;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker raceStateTracker) => this.raceStateTracker = raceStateTracker;

    private void Awake()
    {
        follower.SetProperties(car, camera);      
        fovCorrector.SetProperties(car, camera);
        shaker.SetProperties(car, camera);
        motionBlur.SetProperties(car, camera);
        vignette.SetProperties(car, camera);
    }

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Completed += OnCompleted;

        follower.enabled = false;
        pathFollower.enabled = true;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Completed -= OnCompleted;
    }

    private void OnPreparationStarted()
    {
        follower.enabled = true;
        pathFollower.enabled = false;
    }

    private void OnCompleted()
    {
        follower.enabled = false;

        pathFollower.enabled = true;
        pathFollower.StartMoveToNearestPoint();
        pathFollower.SetLookTarget(car.transform);
    }
}
