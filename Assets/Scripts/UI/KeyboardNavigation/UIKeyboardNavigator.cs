using UnityEngine;

[RequireComponent(typeof(UISelectableButtonContainer))]
public class UIKeyboardNavigator : MonoBehaviour
{
    private UISelectableButtonContainer buttonContainer;

    private void Start()
    {
        buttonContainer = GetComponent<UISelectableButtonContainer>();
    }

    private void Update()
    {
        if (buttonContainer.Type == UISelectableButtonContainer.LayoutType.Horizontal)
        {
            if (Input.GetKeyDown(KeyCode.E))
                SelectNext();

            if (Input.GetKeyDown(KeyCode.Q))
                SelectPrevious();
        }

        if (buttonContainer.Type == UISelectableButtonContainer.LayoutType.Vertical)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
                SelectNext();

            if (Input.GetKeyDown(KeyCode.UpArrow))
                SelectPrevious();
        }

        if (buttonContainer.Type == UISelectableButtonContainer.LayoutType.Grid)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                SelectNext();

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                for (int i = 0; i < buttonContainer.ConstraintCount; i++)
                    SelectNext();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                SelectPrevious();

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                for (int i = 0; i < buttonContainer.ConstraintCount; i++)
                    SelectPrevious();
            }
        }
    }

    private void SelectNext()
    {
        buttonContainer.SelectNext();
    }

    private void SelectPrevious()
    {
        buttonContainer.SelectPrevious();
    }
}
