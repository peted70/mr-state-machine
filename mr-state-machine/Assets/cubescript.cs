using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using EventTrigger = UnityEngine.EventSystems.EventTrigger;

public class cubescript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //var trigger = gameObject.GetComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerDown;
        //entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        //trigger.triggers.Add(entry);
    }

    public void OnPointerDownDelegate(PointerEventData data)
    {
        Debug.Log("OnPointerDownDelegate called.");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
