using UnityEngine;
using UnityEngine.UI;

public class CarSpeedIndicator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Text text;

    private void Update()
    {
        text.text = car.LinearVelocity.ToString("F0"); // "F0" округляет число до запятой, оставляя только целую часть.
    }
}
