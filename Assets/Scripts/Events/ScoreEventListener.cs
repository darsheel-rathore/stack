using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Serializable UnityEvent that takes a Component and an object as arguments
[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> { }

// ScoreEventListener class listens to a ScoreEvent and invokes a UnityEvent when the score changes
public class ScoreEventListener : MonoBehaviour
{
    // Reference to the ScoreEvent to listen to
    public ScoreEvent _scoreEvent;

    // UnityEvent to be invoked when the score changes
    public CustomGameEvent _unityEvent;

    // Called when this component is enabled
    private void OnEnable()
    {
        // Register this ScoreEventListener with the ScoreEvent
        _scoreEvent.Register(this);
    }

    // Called when this component is disabled
    private void OnDisable()
    {
        // Deregister this ScoreEventListener from the ScoreEvent
        _scoreEvent.DeRegister(this);
    }

    // Function to invoke the UnityEvent with the specified sender and data
    public void Invoke(Component sender, object data)
    {
        // Invoke the UnityEvent with the provided sender and data
        _unityEvent.Invoke(sender, data);
    }
}

