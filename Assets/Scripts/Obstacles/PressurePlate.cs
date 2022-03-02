using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PressurePlate : MonoBehaviour
{
    public PressurePlateEvent onSteppedOn;
    public PressurePlateEvent onSteppedOff;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (onSteppedOn != null && onSteppedOn.canTriggerMultipleTimes || !onSteppedOn.hasBeenTriggered)
            {
                // Call our event if it isn't null
                onSteppedOn.onTrigger?.Invoke();
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (onSteppedOff != null && onSteppedOff.canTriggerMultipleTimes || !onSteppedOff.hasBeenTriggered)
        {
            // Call our event if it isn't null
            onSteppedOff.onTrigger?.Invoke();
        }
    }
}

/// <summary>
/// Represents an event triggered when a PressurePlate is stepped on or stepped off of.
/// Contains an event to trigger and a bool determining if the event can be triggered multiple times or not.
/// </summary>
[System.Serializable]
public class PressurePlateEvent
{
    public UnityEvent onTrigger;
    public bool canTriggerMultipleTimes;
    public bool hasBeenTriggered;
}
