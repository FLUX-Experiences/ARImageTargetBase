using UnityEngine;
using UnityEngine.Events;

public class EventTriggerExtend : MonoBehaviour
{
    public UnityEvent OnStarted;
    public UnityEvent OnRemoved;

    void Start()
    {
        
    }
    private void OnEnable()
    {
        OnStarted.Invoke();
    }

    private void OnDisable()
    {
        OnRemoved.Invoke();
    }
}