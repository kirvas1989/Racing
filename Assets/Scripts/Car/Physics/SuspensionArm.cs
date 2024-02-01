using UnityEngine;

public class SuspensionArm : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float factor;
    [SerializeField] private float correctAxe;

    private float baseOffset;

    private void Start()
    {
        baseOffset = target.localPosition.z; 
    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(0, ((target.localPosition.z - baseOffset) * factor) + correctAxe, 0);
    }
}
