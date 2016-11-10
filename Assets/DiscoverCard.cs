using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(EventTrigger))]
public class DiscoverCard : MonoBehaviour {

    public UnityEvent ChangeDiscoverCard;

    public bool active = false;
    private Image img;

    public static List<DiscoverCard> discoverList = null;

    void Start () {

        if(discoverList == null)
        {
            discoverList = new List<DiscoverCard>();
            transform.parent.GetComponentsInChildren(true, discoverList);
        }
        img = GetComponent<Image>();
        if (active)
        {
            ChangeDiscoverCard.Invoke();
            img.color = Color.white;
        }
        else img.color = Color.gray;

        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { Clicked(); });
        trigger.triggers.Add(entry);
    }

    public void Clicked()
    {
        ChangeDiscoverCard.Invoke();
        
        foreach(DiscoverCard dc in discoverList)
        {
            dc.active = false;
            dc.img.color = Color.gray;
        }

        active = true;
        img.color = Color.white;
    }
}
