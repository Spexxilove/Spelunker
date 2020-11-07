using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterEventDispatcher : MonoBehaviour
{
    [SerializeField]
    private GameEvent TriggeredEvent;
    
    [SerializeField]
    private string colliderTag;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // trigger event if tag is empty or matches
        if (string.IsNullOrEmpty(colliderTag) || collider.gameObject.tag.Equals(colliderTag))
        {
            TriggeredEvent.Raise();
        }
    }
}
