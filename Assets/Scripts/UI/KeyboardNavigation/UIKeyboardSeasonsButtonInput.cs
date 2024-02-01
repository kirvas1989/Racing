using UnityEngine;

public class UIKeyboardSeasonsButtonInput : MonoBehaviour
{
    private UISelectableButton seasonButton;

    [SerializeField] private UIKeyboardNavigator menuItemsBar;
    [SerializeField] private UIKeyboardCloseInput closeButton;

    void Start()
    {
        seasonButton = GetComponent<UISelectableButton>();

        closeButton.Closed += OnSeasonPanelClosed;
    }

    private void OnDestroy()
    {
        closeButton.Closed -= OnSeasonPanelClosed;
    }

    public void OnSeasonPanelClosed()
    {
        menuItemsBar.enabled = true;
    }

    public void OnSeasonPanelOpened()
    {
        menuItemsBar.enabled = false;
    }

    void Update()
    {
        if (seasonButton.Focus == true)
        {
            if (Input.GetKeyDown(KeyCode.Return) ||
                Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                seasonButton.OnClick?.Invoke();
                OnSeasonPanelOpened();           
            }
        }
    }
}
