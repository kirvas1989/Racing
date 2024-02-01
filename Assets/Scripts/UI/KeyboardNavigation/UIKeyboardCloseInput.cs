using UnityEngine;
using UnityEngine.Events;

public class UIKeyboardCloseInput : MonoBehaviour
{
    public UnityAction Closed;
   
    private UIButton button;

    void Start()
    {
        button = GetComponent<UIButton>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            button.OnClick?.Invoke();
            Closed?.Invoke();
        }
    }
}
