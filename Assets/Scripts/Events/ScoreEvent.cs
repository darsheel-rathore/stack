using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This attribute allows creating instances of this scriptable object from the Unity Editor
[CreateAssetMenu(fileName = "Score Event", menuName = "Score Event")]
public class ScoreEvent : ScriptableObject
{
    // A HashSet to store the listeners for this score event
    private HashSet<ScoreEventListener> listeners = new HashSet<ScoreEventListener>();

    // Register a listener for this score event
    public void Register(ScoreEventListener scoreEventListener) => listeners.Add(scoreEventListener);

    // Deregister a listener from this score event
    public void DeRegister(ScoreEventListener scoreEventListener) => listeners.Remove(scoreEventListener);

    // Raise the score event and notify all registered listeners
    public void RaiseEvent(Component sender, object data)
    {
        // Iterate through the registered listeners and invoke their event handling method
        foreach (var scoreEventListener in listeners)
        {
            // Call the Invoke method of the ScoreEventListener and pass the sender and data as parameters
            scoreEventListener.Invoke(sender, data);
        }
    }
}
