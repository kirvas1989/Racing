using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CarCameraVignette : CarCameraComponent
{
    [SerializeField] private float baseIntensityValue = 0f;
    [SerializeField] private float intensityModifier = 0.3f;
    [SerializeField] [Range(0.0f, 1.0f)] private float normalizeCarSpeed;

    private PostProcessVolume postProcessVolume;
    private Vignette vignetteLayer;

    void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out vignetteLayer);
    }

    void Update()
    {
        if (car.NormalizeLinearVelocity >= normalizeCarSpeed)
        {
            vignetteLayer.intensity.value = intensityModifier * car.NormalizeLinearVelocity;
        }
        else
        {
            vignetteLayer.intensity.value = baseIntensityValue;
        }
    }
}
