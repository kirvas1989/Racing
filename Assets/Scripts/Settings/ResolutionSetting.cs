using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class ResolutionSetting : Setting
{
    [SerializeField]
    private Vector2Int[] avaliableResolutions = new Vector2Int[]
    {
        new Vector2Int(800, 600),
        new Vector2Int(1024, 768),
        new Vector2Int(1280, 720),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080),
    };

    private int currentResolutionIndex = 0;

    public override bool isMinValue { get => currentResolutionIndex == 0; }
    public override bool isMaxValue { get => currentResolutionIndex == avaliableResolutions.Length - 1; }

    public override void SetNextValue()
    {
        if (isMaxValue == false)
        {
            currentResolutionIndex++;   
        }
    }

    public override void SetPreviousValue()
    {
        if (isMinValue == false)
        {
            currentResolutionIndex--;
        }
    }

    public override object GetValue()
    {
        return avaliableResolutions[currentResolutionIndex];
    }

    public override string GetStringValue()
    {
        return avaliableResolutions[currentResolutionIndex].x + "x" + avaliableResolutions[currentResolutionIndex].y;
    }

    public override void Apply()
    {
        Screen.SetResolution(avaliableResolutions[currentResolutionIndex].x, avaliableResolutions[currentResolutionIndex].y, true); // можно вкл./выкл. полноэкранный режим

        Save();
    }

    public override void Load()
    {
        currentResolutionIndex = PlayerPrefs.GetInt(title, avaliableResolutions.Length - 1);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(title, currentResolutionIndex);
    }
}

