using UnityEngine;
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
    Deathrattle, Taunt, Overload
}

public enum TriClass
{
    No, Kabal, Goons, Lotus
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
    public int manaCost = 3;
    public List<Ability> abilities = new List<Ability>();
    public TriClass clan = TriClass.No;

    [HideInInspector]
	public Transform originalParent;

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
	void Awake()
	{
		originalParent = transform.parent;
	}
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
        {
            SpriteRenderer rend = GetComponentInChildren<SpriteRenderer>();
            if (rend != null)
            {
                img.sprite = rend.sprite;
                DestroyImmediate(rend.gameObject);
            }
            transform.name = img.sprite.name;
        }

    }

	[HideInInspector]
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

    //these will need custom functions for discover class cards

    private void AddAsTarget()
    {
        if (hero == Hero.Neutral)
        {
            calculator.neutralTargets++;
            switch(clan)
            {
                case TriClass.No:
                    break;
                case TriClass.Goons:
                    calculator.ClassTargetsSpecial(Hero.Hunter, 1);
                    calculator.ClassTargetsSpecial(Hero.Paladin, 1);
                    calculator.ClassTargetsSpecial(Hero.Warrior, 1);
                    break;
                case TriClass.Kabal:
                    calculator.ClassTargetsSpecial(Hero.Mage, 1);
                    calculator.ClassTargetsSpecial(Hero.Priest, 1);
                    calculator.ClassTargetsSpecial(Hero.Warlock, 1);
                    break;
                case TriClass.Lotus:
                    calculator.ClassTargetsSpecial(Hero.Druid, 1);
                    calculator.ClassTargetsSpecial(Hero.Rogue, 1);
                    calculator.ClassTargetsSpecial(Hero.Shaman, 1);
                    break;
            }
            calculator.UpdatePercentage();
        }
        else
        {
            calculator.classTargets++;
            calculator.ClassTargetsSpecial(hero, 1);
            calculator.UpdatePercentage();
        }
    }

    private void RemoveAsTarget()
    {
        if (hero == Hero.Neutral)
        {
            calculator.neutralTargets--;
            switch (clan)
            {
                case TriClass.No:
                    break;
                case TriClass.Goons:
                    calculator.ClassTargetsSpecial(Hero.Hunter, -1);
                    calculator.ClassTargetsSpecial(Hero.Paladin, -1);
                    calculator.ClassTargetsSpecial(Hero.Warrior, -1);
                    break;
                case TriClass.Kabal:
                    calculator.ClassTargetsSpecial(Hero.Mage, -1);
                    calculator.ClassTargetsSpecial(Hero.Priest, -1);
                    calculator.ClassTargetsSpecial(Hero.Warlock, -1);
                    break;
                case TriClass.Lotus:
                    calculator.ClassTargetsSpecial(Hero.Druid, -1);
                    calculator.ClassTargetsSpecial(Hero.Rogue, -1);
                    calculator.ClassTargetsSpecial(Hero.Shaman, -1);
                    break;
            }
            calculator.UpdatePercentage();
        }
        else
        {
            calculator.classTargets--;
            calculator.ClassTargetsSpecial(hero, -1);
            calculator.UpdatePercentage();
        }
    }

    public bool usableByClass(Hero hero)
    {
        if (clan == TriClass.No)
            return true;
        else if (clan == TriClass.Goons && (hero == Hero.Hunter || hero == Hero.Paladin || hero == Hero.Warrior))
        {
            return true;
        }
        else if (clan == TriClass.Kabal && (hero == Hero.Mage || hero == Hero.Priest || hero == Hero.Warlock))
        {
            return true;
        }
        else if (clan == TriClass.Lotus && (hero == Hero.Druid || hero == Hero.Rogue || hero == Hero.Shaman))
        {
            return true;
        }
        return false;
    }
}
