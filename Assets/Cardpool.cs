using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Cardpool : MonoBehaviour {

    Transform NeutralContainer;
    Transform DruidContainer;
    Transform HunterContainer;
    Transform MageContainer;
    Transform PaladinContainer;
    Transform RogueContainer;
    Transform PriestContainer;
    Transform ShamanContainer;
    Transform WarlockContainer;
    Transform WarriorContainer;
    List<Card> NeutralList = new List<Card>();
    List<Card> DruidList = new List<Card>();
    List<Card> HunterList = new List<Card>();
    List<Card> MageList = new List<Card>();
    List<Card> PaladinList = new List<Card>();
    List<Card> RogueList = new List<Card>();
    List<Card> PriestList = new List<Card>();
    List<Card> ShamanList = new List<Card>();
    List<Card> WarlockList = new List<Card>();
    List<Card> WarriorList = new List<Card>();

    List<Card> PoolOfCards = new List<Card>();

    private static DiscoverCalculator _calculator;
    private static DiscoverCalculator calculator
    {
        get
        {
            if (_calculator == null)
            {
                _calculator = GameObject.Find("Calculator").GetComponent<DiscoverCalculator>();
            }
            return _calculator;
        }
        set
        {
            _calculator = value;
        }
    }

    void Start ()
    {
        Transform AllCardContainer = GameObject.Find("AllCards").transform;

        NeutralContainer = AllCardContainer.Find("Neutral");
        NeutralContainer.GetComponentsInChildren(true, NeutralList);

        DruidContainer = AllCardContainer.Find("Druid");
        DruidContainer.GetComponentsInChildren(true, DruidList);

        HunterContainer = AllCardContainer.Find("Hunter");
        HunterContainer.GetComponentsInChildren(true, HunterList);

        MageContainer = AllCardContainer.Find("Mage");
        MageContainer.GetComponentsInChildren(true, MageList);

        PaladinContainer = AllCardContainer.Find("Paladin");
        PaladinContainer.GetComponentsInChildren(true, PaladinList);

        RogueContainer = AllCardContainer.Find("Rogue");
        RogueContainer.GetComponentsInChildren(true, RogueList);

        PriestContainer = AllCardContainer.Find("Priest");
        PriestContainer.GetComponentsInChildren(true, PriestList);

        ShamanContainer = AllCardContainer.Find("Shaman");
        ShamanContainer.GetComponentsInChildren(true, ShamanList);

        WarlockContainer = AllCardContainer.Find("Warlock");
        WarlockContainer.GetComponentsInChildren(true, WarlockList);

        WarriorContainer = AllCardContainer.Find("Warrior");
        WarriorContainer.GetComponentsInChildren(true, WarriorList);

		AllCardContainer.gameObject.SetActive (false);
	}

	void Update () {
	
	}

    public int neutrals = 0;
    public int classCards = 0;

    void ResetCardPoolStatus()
    {
        neutrals = 0;
        classCards = 0;
        foreach(Card card in PoolOfCards)
        {
			card.Clear();
			card.transform.SetParent (card.originalParent, false);
        }
        PoolOfCards.Clear();
    }

    public void DiscoverSpells(Hero hero)
    {
        ResetCardPoolStatus();
        foreach (Card card in CardList(hero))
        {

            if (card.type == Type.Spell)
            {
                classCards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Neutral))
        {
            if (card.type == Type.Spell)
            {
                neutrals++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }

        calculator.neutrals = neutrals;
        calculator.neutralTargets = 0;
        calculator.classCards = classCards;
        calculator.classTargets = 0;

		PoolOfCards.Sort (new CardSorter());

		SetupCardPositions();
    }

    public void Discover3Mana(Hero hero)
    {
        ResetCardPoolStatus();
        foreach (Card card in CardList(hero))
        {
            if (card.manaCost == 3)
            {
                classCards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Neutral))
        {
            if (card.manaCost == 3)
            {
                neutrals++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }

        calculator.neutrals = neutrals;
        calculator.neutralTargets = 0;
        calculator.classCards = classCards;
        calculator.classTargets = 0;

        PoolOfCards.Sort(new CardSorter());

        SetupCardPositions();
    }

    //sort by mana, keeping nameorder (non destructive) and neutral weighting more than class
    public class CardSorter : IComparer<Card>
	{
		public int Compare(Card x, Card y)
		{
			int comp = y.hero.CompareTo (x.hero);
			if (comp == 0)
				return (x.manaCost.CompareTo (y.manaCost));
			else
				return comp;
		}
	}


	void SetupCardPositions()
	{
        int width = 280;
		int height = 380;
        for (int i = 0; i < PoolOfCards.Count; i++)
		{
            PoolOfCards[i].transform.localPosition = new Vector2( width*(i/2), -height*(i%2) );
		}
	}

    List<Card> CardList(Hero hero)
    {
        if (hero == Hero.Neutral)
            return NeutralList;
        else if (hero == Hero.Druid)
            return DruidList;
        else if (hero == Hero.Hunter)
            return HunterList;
        else if (hero == Hero.Mage)
            return MageList;
        else if (hero == Hero.Paladin)
            return PaladinList;
        else if (hero == Hero.Priest)
            return PriestList;
        else if (hero == Hero.Rogue)
            return RogueList;
        else if (hero == Hero.Shaman)
            return ShamanList;
        else if (hero == Hero.Warlock)
            return WarlockList;
        else if (hero == Hero.Warrior)
            return WarriorList;

        Debug.Log("hero does not exist, please ensue panic");
        return null;
    }
}
