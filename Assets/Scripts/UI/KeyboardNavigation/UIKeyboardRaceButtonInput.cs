
using UnityEngine;

public class UIKeyboardRaceButtonInput : MonoBehaviour
{
    private UIRaceButton raceButton;

    void Start()
    {
        raceButton = GetComponent<UIRaceButton>();
    }

    void Update()
    {
        if (raceButton.Focus == true)
        {
            if (Input.GetKeyDown(KeyCode.Return) ||
                Input.GetKeyDown(KeyCode.KeypadEnter))
                raceButton.LoadLevel();
        }
    }
}
