using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] protected bool interactible;

    private bool focus = false;
    public bool Focus => focus;

    public UnityEvent OnClick;

    public event UnityAction<UIButton> PointerEnter;
    public event UnityAction<UIButton> PointerExit;
    public event UnityAction<UIButton> PointerClick;

    public virtual void SetFocus()
    {
        if (interactible == false) return;

        focus = true;
    }

    public virtual void SetUnFocus()
    {
        if (interactible == false) return;

        focus = false;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (interactible == false) return;

        PointerEnter?.Invoke(this);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (interactible == false) return;

        PointerExit?.Invoke(this);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (interactible == false) return;

        PointerClick?.Invoke(this);
        OnClick?.Invoke();
    }
}
