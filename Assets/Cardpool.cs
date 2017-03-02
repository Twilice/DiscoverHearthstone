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

    public bool standard = true;

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
            if((standard && card.set == Set.Wild) == false)
            if (card.type == Type.Spell)
            {
                classCards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Neutral))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.type == Type.Spell)
            if(card.usableByClass(hero))
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
        calculator.UpdatePercentage = calculator.UpdatePercentageNormal;
        calculator.UpdatePercentage();

        PoolOfCards.Sort (cardSorter);

		SetupCardPositions();
    }

    public void Discover3Mana(Hero hero)
    {
        ResetCardPoolStatus();
        foreach (Card card in CardList(hero))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.manaCost == 3)
            {
                classCards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Neutral))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.manaCost == 3)
            if(card.usableByClass(hero))
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
        calculator.UpdatePercentage = calculator.UpdatePercentageNormal;
        calculator.UpdatePercentage();

        PoolOfCards.Sort(cardSorter);

        SetupCardPositions();
    }

    public void DiscoverDeathrattle(Hero hero)
    {
        ResetCardPoolStatus();
        foreach (Card card in CardList(hero))
        {
            if((standard && card.set == Set.Wild) == false)
                foreach (Ability a in card.abilities)
                {
                    if (a == Ability.Deathrattle)
                    {
                        classCards++;
                        PoolOfCards.Add(card);
                        card.transform.SetParent(this.transform, false);
                    }
                }
        }
        foreach (Card card in CardList(Hero.Neutral))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.usableByClass(hero))
                foreach (Ability a in card.abilities)
                {
                    if (a == Ability.Deathrattle)
                    {
                        neutrals++;
                        PoolOfCards.Add(card);
                        card.transform.SetParent(this.transform, false);
                    }
                }
        }

        calculator.neutrals = neutrals;
        calculator.neutralTargets = 0;
        calculator.classCards = classCards;
        calculator.classTargets = 0;
        calculator.UpdatePercentage = calculator.UpdatePercentageNormal;
        calculator.UpdatePercentage();

        PoolOfCards.Sort(cardSorter);

        SetupCardPositions();
    }

    public void DiscoverTaunt(Hero hero)
    {
        ResetCardPoolStatus();
        foreach (Card card in CardList(hero))
        {
            if ((standard && card.set == Set.Wild) == false)
                foreach (Ability a in card.abilities)
                {
                    if (a == Ability.Taunt)
                    {
                        classCards++;
                        PoolOfCards.Add(card);
                        card.transform.SetParent(this.transform, false);
                    }
                }
        }
        foreach (Card card in CardList(Hero.Neutral))
        {
            if ((standard && card.set == Set.Wild) == false)
            if (card.usableByClass(hero))
                foreach (Ability a in card.abilities)
                {
                    if (a == Ability.Taunt)
                    {
                        neutrals++;
                        PoolOfCards.Add(card);
                        card.transform.SetParent(this.transform, false);
                    }
                }
        }

        calculator.neutrals = neutrals;
        calculator.neutralTargets = 0;
        calculator.classCards = classCards;
        calculator.classTargets = 0;
        calculator.UpdatePercentage = calculator.UpdatePercentageNormal;
        calculator.UpdatePercentage();

        PoolOfCards.Sort(cardSorter);

        SetupCardPositions();
    }

  
    public void DiscoverOverload(Hero hero)
    {
        ResetCardPoolStatus();
        // assume every class discovers the shaman cards?
        foreach (Card card in CardList(Hero.Shaman))
        {
            if ((standard && card.set == Set.Wild) == false)
                foreach (Ability a in card.abilities)
                {
                    if (a == Ability.Overload)
                    {
                        classCards++;
                        PoolOfCards.Add(card);
                        card.transform.SetParent(this.transform, false);
                    }
                }
        }
        foreach (Card card in CardList(Hero.Neutral))
        {
            if ((standard && card.set == Set.Wild) == false)
            if (card.usableByClass(hero))
                foreach (Ability a in card.abilities)
                {
                    if (a == Ability.Overload)
                    {
                        neutrals++;
                        PoolOfCards.Add(card);
                        card.transform.SetParent(this.transform, false);
                    }
                }
        }

        calculator.neutrals = neutrals;
        calculator.neutralTargets = 0;
        calculator.classCards = classCards;
        calculator.classTargets = 0;
        calculator.UpdatePercentage = calculator.UpdatePercentageNormal;
        calculator.UpdatePercentage();

        PoolOfCards.Sort(cardSorter);

        SetupCardPositions();
    }

    public void DiscoverMinion(Hero hero)
    {
        ResetCardPoolStatus();
        foreach (Card card in CardList(hero))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.type == Type.Minion)
            {
                classCards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Neutral))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.type == Type.Minion)
            if (card.usableByClass(hero))
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
        calculator.UpdatePercentage = calculator.UpdatePercentageNormal;
        calculator.UpdatePercentage();

        PoolOfCards.Sort(cardSorter);

        SetupCardPositions();
    }

    public void Discover1Mana(Hero hero)
    {
        ResetCardPoolStatus();
        foreach (Card card in CardList(hero))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.manaCost == 1)
            {
                classCards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Neutral))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.manaCost == 1)
            if (card.usableByClass(hero))
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
        calculator.UpdatePercentage = calculator.UpdatePercentageNormal;
        calculator.UpdatePercentage();

        PoolOfCards.Sort(cardSorter);

        SetupCardPositions();
    }

    public void DiscoverDragon(Hero hero)
    {
        ResetCardPoolStatus();
        foreach (Card card in CardList(hero))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.tribe == Tribe.Dragon)
            {
                classCards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Neutral))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.tribe == Tribe.Dragon)
            if (card.usableByClass(hero))
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
        calculator.UpdatePercentage = calculator.UpdatePercentageNormal;
        calculator.UpdatePercentage();

        PoolOfCards.Sort(cardSorter);

        SetupCardPositions();
    }

    public void DiscoverBeast(Hero hero)
    {
        ResetCardPoolStatus();
        foreach (Card card in CardList(hero))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.tribe == Tribe.Beast)
            {
                classCards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Neutral))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.tribe == Tribe.Beast)
            if (card.usableByClass(hero))
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
        calculator.UpdatePercentage = calculator.UpdatePercentageNormal;
        calculator.UpdatePercentage();

        PoolOfCards.Sort(cardSorter);

        SetupCardPositions();
    }
    public void DiscoverMech(Hero hero)
    {
        ResetCardPoolStatus();
        foreach (Card card in CardList(hero))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.tribe == Tribe.Mech)
            {
                classCards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Neutral))
        {
            if((standard && card.set == Set.Wild) == false)
            if (card.tribe == Tribe.Mech)
            if (card.usableByClass(hero))
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
        calculator.UpdatePercentage = calculator.UpdatePercentageNormal;
        calculator.UpdatePercentage();

        PoolOfCards.Sort(cardSorter);

        SetupCardPositions();
    }

    public void DiscoverKabal(Hero hero)
    {
        ResetCardPoolStatus();
        int class1Cards = 0;
        int class2Cards = 0;
        int class3Cards = 0;

        foreach(Card card in CardList(Hero.Neutral))
        {
            if(card.clan == TriClass.Kabal)
            if ((standard && card.set == Set.Wild) == false)
            {
                class1Cards++;
                class2Cards++;
                class3Cards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Mage))
        {
            if ((standard && card.set == Set.Wild) == false)
            {
                class1Cards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Priest))
        {
            if ((standard && card.set == Set.Wild) == false)
            {
                class2Cards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Warlock))
        {
            if ((standard && card.set == Set.Wild) == false)
            {
                class3Cards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        
        calculator.class1Cards = class1Cards;
        calculator.class1Targets = 0;
        calculator.class2Cards = class2Cards;
        calculator.class2Targets = 0;
        calculator.class3Cards = class3Cards;
        calculator.class3Targets = 0;
        calculator.UpdatePercentage = calculator.UpdatePercentageTriClass;
        calculator.UpdatePercentage();

        PoolOfCards.Sort(cardSorterTriClass);

        SetupCardPositions();
    }

    public void DiscoverGoons(Hero hero)
    {
        ResetCardPoolStatus();
        int class1Cards = 0;
        int class2Cards = 0;
        int class3Cards = 0;

        foreach (Card card in CardList(Hero.Neutral))
        {
            if(card.clan == TriClass.Goons)
            if ((standard && card.set == Set.Wild) == false)
            {
                class1Cards++;
                class2Cards++;
                class3Cards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Hunter))
        {
            if ((standard && card.set == Set.Wild) == false)
            {
                class1Cards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Paladin))
        {
            if ((standard && card.set == Set.Wild) == false)
            {
                class2Cards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Warrior))
        {
            if ((standard && card.set == Set.Wild) == false)
            {
                class3Cards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }

        calculator.class1Cards = class1Cards;
        calculator.class1Targets = 0;
        calculator.class2Cards = class2Cards;
        calculator.class2Targets = 0;
        calculator.class3Cards = class3Cards;
        calculator.class3Targets = 0;
        calculator.UpdatePercentage = calculator.UpdatePercentageTriClass;
        calculator.UpdatePercentage();

        PoolOfCards.Sort(cardSorterTriClass);

        SetupCardPositions();
    }

    public void DiscoverLotus(Hero hero)
    {
        ResetCardPoolStatus();
        int class1Cards = 0;
        int class2Cards = 0;
        int class3Cards = 0;

        foreach (Card card in CardList(Hero.Neutral))
        {
            if(card.clan == TriClass.Lotus)
            if ((standard && card.set == Set.Wild) == false)
            {
                class1Cards++;
                class2Cards++;
                class3Cards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Druid))
        {
            if ((standard && card.set == Set.Wild) == false)
            {
                class1Cards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Rogue))
        {
            if ((standard && card.set == Set.Wild) == false)
            {
                class2Cards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }
        foreach (Card card in CardList(Hero.Shaman))
        {
            if ((standard && card.set == Set.Wild) == false)
            {
                class3Cards++;
                PoolOfCards.Add(card);
                card.transform.SetParent(this.transform, false);
            }
        }

        calculator.class1Cards = class1Cards;
        calculator.class1Targets = 0;
        calculator.class2Cards = class2Cards;
        calculator.class2Targets = 0;
        calculator.class3Cards = class3Cards;
        calculator.class3Targets = 0;
        calculator.UpdatePercentage = calculator.UpdatePercentageTriClass;
        calculator.UpdatePercentage();

        PoolOfCards.Sort(cardSorterTriClass);

        SetupCardPositions();
    }

    public CardSorter cardSorter = new CardSorter();
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
    public CardSorterTriClass cardSorterTriClass = new CardSorterTriClass();
    //sort by mana, keeping nameorder (non destructive) and neutral weighting more than class
    public class CardSorterTriClass : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {

            int comp = x.hero.CompareTo(y.hero);
            if (comp == 0)
                return (x.manaCost.CompareTo(y.manaCost));
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
