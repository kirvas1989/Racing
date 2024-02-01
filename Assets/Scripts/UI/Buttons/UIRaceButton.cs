using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIRaceButton : UISelectableButton, IScriptableObjectProperty
{
    [SerializeField] private RaceInfo raceInfo;
    [SerializeField] private Image icon;
    [SerializeField] private Image lockIcon;
    [SerializeField] private Text title;

    private bool isLocked;

    private void Start()
    {
        ApplyProperty(raceInfo);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        LoadLevel();
    }

    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null) return;

        if (property is RaceInfo == false) return;
        raceInfo = property as RaceInfo;
        
        icon.sprite = raceInfo.Icon;
        title.text = raceInfo.Title;
    }

    public void SetInteractable(bool interactable)
    {
        interactible = interactable;
        lockIcon.enabled = !interactable;
        isLocked = !interactable;
    }

    public void LoadLevel()
    {
        if (raceInfo == null || isLocked) return;

        SceneManager.LoadScene(raceInfo.SceneName);
    }
}
