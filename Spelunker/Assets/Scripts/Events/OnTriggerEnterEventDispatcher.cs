using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterEventDispatcher : MonoBehaviour
{
    [SerializeField]
    private GameEvent TriggeredEvent;

    [Tooltip("Tag the other collider has to have to trigger this event. If none is set any object will trigger the event")]
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
