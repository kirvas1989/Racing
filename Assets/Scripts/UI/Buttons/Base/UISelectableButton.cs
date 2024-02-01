using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISelectableButton : UIButton
{
    [SerializeField] private Image selectImage;

    public UnityEvent OnSelect;
    public UnityEvent OnUnSelect;

    public bool Interactible => interactible;

    public override void SetFocus()
    {
        base.SetFocus();

        selectImage.enabled = true;
        OnSelect?.Invoke();
    }

    public override void SetUnFocus()
    {
        base.SetUnFocus();

        selectImage.enabled = false;
        OnUnSelect?.Invoke();
    }
}
