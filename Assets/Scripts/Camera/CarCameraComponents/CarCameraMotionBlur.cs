using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CarCameraMotionBlur : CarCameraComponent
{
    [SerializeField] private float baseShutterAngle = 0f;
    [SerializeField] private float shutterAngleModifier = 70f;
    [SerializeField][Range(0.0f, 1.0f)] private float normalizeCarSpeed;

    private PostProcessVolume postProcessVolume;
    private MotionBlur blurLayer;

    void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out blurLayer);
    }

    void Update()
    {
        if (car.NormalizeLinearVelocity >= normalizeCarSpeed)
        {
            blurLayer.shutterAngle.value = shutterAngleModifier * car.NormalizeLinearVelocity;
        }
        else
        {
            blurLayer.shutterAngle.value = baseShutterAngle;
        }
    }
}
