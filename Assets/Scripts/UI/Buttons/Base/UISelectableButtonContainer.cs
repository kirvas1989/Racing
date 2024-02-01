using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectableButtonContainer : MonoBehaviour
{
    public enum LayoutType
    {
        Vertical,
        Horizontal,
        Grid
    }

    [SerializeField] private LayoutType layoutType;
    public LayoutType Type => layoutType;

    [SerializeField] private bool setAuto = true;

    [SerializeField] private Transform buttonContainer;

    public bool Interactible = true;
    public void SetInteractible(bool interactible) => Interactible = interactible;

    private UISelectableButton[] buttons;

    private LayoutGroup layout;

    private int selectButtonIndex = 0;

    private int constraintCount;
    public int ConstraintCount => constraintCount;

    private static int minInteractibleButtonsAmount = 2;

    private void Start()
    {
        if (setAuto)
            SetLayoutType();

        buttons = buttonContainer.GetComponentsInChildren<UISelectableButton>();

        if (buttons == null)
            Debug.Log("Buttons list is empty");

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter += OnPointerEnter;
        }

        if (Interactible == false) return;

        buttons[selectButtonIndex].SetFocus();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter -= OnPointerEnter;
        }
    }

    private void OnPointerEnter(UIButton button)
    {
        SelectButton(button);
    }

    private void SetLayoutType()
    {
        layout = buttonContainer.GetComponent<LayoutGroup>();

        if (layout is HorizontalLayoutGroup)
            layoutType = LayoutType.Horizontal;

        if (layout is VerticalLayoutGroup)
            layoutType = LayoutType.Vertical;

        if (layout is GridLayoutGroup)
        {
            layoutType = LayoutType.Grid;

            var gridLayout = layout.GetComponent<GridLayoutGroup>();
            constraintCount = gridLayout.constraintCount;
        }
    }

    private void SelectButton(UIButton button)
    {
        if (Interactible == false) return;

        buttons[selectButtonIndex].SetUnFocus();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (button == buttons[i])
            {
                selectButtonIndex = i;
                button.SetFocus();
                break;
            }
        }
    }

    private int InteractibleButtonsAmount()
    {
        int amount = 0;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].Interactible)
                amount++;
        }

        return amount;
    }

    public void SelectNext()
    {
        if (InteractibleButtonsAmount() < minInteractibleButtonsAmount) return;

        List<UISelectableButton> nextButtons = new();

        int focusIndex = 0;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].Focus)
            {
                nextButtons.Add(buttons[i]);
                focusIndex = i;
                break;
            }
        }

        for (int j = focusIndex + 1; j < buttons.Length; j++)
        {
            if (buttons[j].Interactible)
                nextButtons.Add(buttons[j]);
        }

        for (int k = 0; k < focusIndex; k++)
        {
            if (buttons[k].Interactible)
                nextButtons.Add(buttons[k]);
        }

        nextButtons[0].SetUnFocus();
        nextButtons[1].SetFocus();
    }

    public void SelectPrevious()
    {
        if (InteractibleButtonsAmount() < minInteractibleButtonsAmount) return;

        List<UISelectableButton> previousButtons = new();

        int focusIndex = 0;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].Focus)
            {
                previousButtons.Add(buttons[i]);
                focusIndex = i;
                break;
            }
        }

        for (int j = focusIndex - 1; j >= 0; j--)
        {
            if (buttons[j].Interactible)
                previousButtons.Add(buttons[j]);
        }

        for (int k = buttons.Length - 1; k > focusIndex; k--)
        {
            if (buttons[k].Interactible)
                previousButtons.Add(buttons[k]);
        }

        previousButtons[0].SetUnFocus();
        previousButtons[1].SetFocus();
    }
}

