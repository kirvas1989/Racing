using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class WindSound : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private float baseVolume = 0f;
    [SerializeField] private float volumeModifier;
    [SerializeField] private float basePitch = 0f;
    [SerializeField] private float pitchModifier = 3f;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float normalizeCarSpeed;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (car.NormalizeLinearVelocity >= normalizeCarSpeed)
        {
            audioSource.volume = volumeModifier * car.NormalizeLinearVelocity;
            audioSource.pitch = pitchModifier * car.NormalizeLinearVelocity;
        }
        else
        {
            audioSource.volume = baseVolume;
            audioSource.pitch = basePitch;
        }
    }
}
