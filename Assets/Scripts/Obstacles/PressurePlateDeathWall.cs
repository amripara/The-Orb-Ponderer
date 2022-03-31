using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PressurePlateDeathWall : MonoBehaviour
{
    public PressurePlateEvent onSteppedOn;
    public PressurePlateEvent onSteppedOff;
    [SerializeField] private Material matChanged;
    [SerializeField] private Material originalMat;
    private float matChanger = 0;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (onSteppedOn != null && onSteppedOn.canTriggerMultipleTimes || !onSteppedOn.hasBeenTriggered)
            {
                // Call our event if it isn't null
                onSteppedOn.onTrigger?.Invoke();
                if (matChanger == 0)
                {
                    gameObject.GetComponent<MeshRenderer>().material = matChanged;
                    matChanger = 1;
                } else if (matChanger == 1)
                {
                    gameObject.GetComponent<MeshRenderer>().material = originalMat;
                    matChanger = 0;
                }
                Sounds.PlaySound(Sounds.Sound.SteamVent_PressurePlate);
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
