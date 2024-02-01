using UnityEngine;

public class UIKeyboardAcceptInput : MonoBehaviour
{
    private UISelectableButton button;

    void Start()
    {
        button = GetComponent<UISelectableButton>();
    }

    void Update()
    {
        if (button.Focus == true)
        {
            if (Input.GetKeyDown(KeyCode.Return) || 
                Input.GetKeyDown(KeyCode.KeypadEnter))
                button.OnClick?.Invoke();
        }
    }
}

