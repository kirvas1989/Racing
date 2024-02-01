using UnityEngine;

public class UIKeyboardSettingButtonInput : MonoBehaviour
{
    [SerializeField] private UIButton previousButton;
    [SerializeField] private UIButton nextButton;

    private UISettingButton settingButton;

    private void Start()
    {
        settingButton = GetComponent<UISettingButton>();
    }

    private void Update()
    {
        if (settingButton.Focus == true)
        {
            if (Input.GetKeyDown(KeyCode.Minus) ||
                Input.GetKeyDown(KeyCode.KeypadMinus) ||
                Input.GetKeyDown(KeyCode.LeftArrow))
                previousButton.OnClick?.Invoke();

            if (Input.GetKeyDown(KeyCode.Equals) ||
                Input.GetKeyDown(KeyCode.KeypadPlus) ||
                Input.GetKeyDown(KeyCode.RightArrow))
                nextButton.OnClick?.Invoke();
        }
    }
}
