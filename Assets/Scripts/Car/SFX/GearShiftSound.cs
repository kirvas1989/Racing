using UnityEngine;

public class GearShiftSound : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private float speedTreshold;

    private AudioSource audioSource;
    private AudioClip clip;
    private float timer = 0.5f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clip = audioSource.clip;
        car.GearChanged += OnGearChanged;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) timer = 0;
        }
    }

    private void OnDestroy()
    {
        car.GearChanged -= OnGearChanged;
    }

    private void OnGearChanged(string gearName)
    {
        if (timer == 0)
        {
            if (gearName == "R")
            {
                audioSource.Play();
                timer = 1f;
            }

            if (car.LinearVelocity > speedTreshold)
            {
                audioSource.PlayOneShot(clip);

                if (gearName == "1")
                    timer = 1f;
                else
                    timer = 0.1f;              
            }
        }
    }
}
