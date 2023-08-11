using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score Event", menuName = "Score Event"), ]
public class ScoreEvent : ScriptableObject
{
    private HashSet<ScoreEventListner> listners = new HashSet<ScoreEventListner>();
    public void Register(ScoreEventListner scoreEventListner) => listners.Add(scoreEventListner); 
    public void DeRegister(ScoreEventListner scoreEventListner) => listners.Remove(scoreEventListner);
    public void RaiseEvent(Component sender, object data)
    {
        foreach (var scoreEvent in listners)
            scoreEvent.Invoke(sender, data);
    }
}
