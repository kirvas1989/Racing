using System.Data;
using UnityEngine;

public class RaceUnLocker : MonoBehaviour
{
    [SerializeField] private UIRaceButton[] buttons;

    private RaceCompletion raceCompletion;

    private void Start()
    {
        var drawLevel = 0;
        var score = 1;

        while (score != 0 && drawLevel < buttons.Length &&
            RaceCompletion.Instance.TryIndex(drawLevel, out var raceInfo, out var playerRecordTime, out var isLocked))
        {
            buttons[drawLevel].SetInteractable(!isLocked);
            drawLevel += 1;
        }

        for (int i = drawLevel; i < buttons.Length; i++)
        {
            buttons[i].SetInteractable(false);
        }
    }
}
