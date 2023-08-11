using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> {}

public class ScoreEventListner : MonoBehaviour
{
    public ScoreEvent _scoreEvent;
    public CustomGameEvent _unityEvent;

    private void OnEnable()
    {
        _scoreEvent.Register(this);
    }

    private void OnDisable()
    {
        _scoreEvent.DeRegister(this);
    }

    public void Invoke(Component sender, object data)
    {
        _unityEvent.Invoke(sender, data);
    }
}
