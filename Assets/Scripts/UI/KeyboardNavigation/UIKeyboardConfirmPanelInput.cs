using UnityEngine;
using UnityEngine.UI;

public class UIKeyboardConfirmPanelInput : MonoBehaviour
{
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button declineButton;
    [SerializeField] private UIKeyboardNavigator menuItemsBar;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            menuItemsBar.enabled = false;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                acceptButton.onClick?.Invoke();
                menuItemsBar.enabled = true;
            }
                
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                declineButton.onClick?.Invoke();
                menuItemsBar.enabled = true;
            }  
        }
    }
}
