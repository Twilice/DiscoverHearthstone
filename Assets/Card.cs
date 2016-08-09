﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Hero
{
    Neutral, Druid, Hunter, Mage, Paladin, Rogue, Priest, Shaman, Warlock, Warrior
}

public enum Type
{
    Minion, Spell, Weapon
}

public enum Tribe
{
    None, Beast, Demon, Dragon, Mech, Murloc, Pirate, Totem
}

public enum Ability
{
    Deathrattle
}

public enum Set
{
    Standard, Wild
}
[ExecuteInEditMode]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(EventTrigger))]
public class Card : MonoBehaviour {

    public Set set = 0;
    public Hero hero = 0;
    public Type type = 0;
    public Tribe tribe = 0;
    public List<Ability> abilities = new List<Ability>();
    public int manaCost = 3;

    private static Transform _cardPoolContainer;
    private static Transform cardPoolContainer
    {
        get
        {
            if (_cardPoolContainer == null)
            {
                _cardPoolContainer = GameObject.Find("CardPool").transform;
            }
            return _cardPoolContainer;
        }
        set
        {
            _cardPoolContainer = value;
        }
    }

    private static DiscoverCalculator _calculator;
    private static DiscoverCalculator calculator
    {
        get
        {
            if (_calculator == null)
            {
                _calculator = GameObject.Find("Calculator").transform.GetComponent<DiscoverCalculator>();
            }
            return _calculator;
        }
        set
        {
            _calculator = value;
        }
    }

    private Image img;
	void Start () {
        img = GetComponent<Image>();

        if (Application.isPlaying)
        {
            active = false;
            img.color = Color.gray;
        
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { Clicked(); });
            trigger.triggers.Add(entry);
        }

    }
	
	void Update () {
        if (Application.isPlaying == false && img != null)
            transform.name = img.sprite.name;

    }

    public bool active = true;
    public void Clicked()
    {
        if(transform.parent == cardPoolContainer)
        {
            active = !active;
            if (active)
            {
                img.color = Color.white;
                AddAsTarget();
            }
            else
            {
                RemoveAsTarget();
                img.color = Color.gray;
            }
        }
    }

    public void Clear()
    {
        active = false;
        img.color = Color.gray;
    }

    private void AddAsTarget()
    {
        if (hero == Hero.Neutral)
            calculator.neutralTargets++;
        else
            calculator.classTargets++;
    }

    private void RemoveAsTarget()
    {
        if (hero == Hero.Neutral)
            calculator.neutralTargets--;
        else
            calculator.classTargets--;
    }
}
