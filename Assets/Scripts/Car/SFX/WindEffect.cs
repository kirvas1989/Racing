using UnityEngine;

public class WindEffect : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private GameObject visualModel;
    [SerializeField][Range(0.0f, 1.0f)] private float normalizeCarSpeed;

    private void Update()
    {
        if (car.NormalizeLinearVelocity >= normalizeCarSpeed)
            visualModel.SetActive(true);
        else
            visualModel.SetActive(false);
    }
}
