using UnityEngine;

public class ExhaustShaker : MonoBehaviour
{
    [SerializeField] private float shakeAmount;

    private Vector3 defaultPosition;
    private float timer = 0.5f;

    private void Start()
    {
        defaultPosition = transform.localPosition;
    }

    private void Update()
    {
        if (timer > 0)
        {
            transform.localPosition += Random.insideUnitSphere * shakeAmount * Time.deltaTime;
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                transform.localPosition = defaultPosition;

                timer = 0.5f;
            }
        }
    }
}

