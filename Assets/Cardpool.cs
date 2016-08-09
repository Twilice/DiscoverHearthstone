using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cardpool : MonoBehaviour {

    Transform NeutralContainer;
    Transform MageContainer;
    List<Card> NeutralList = new List<Card>();
    List<Card> MageList = new List<Card>();

    List<Card> PoolOfCards = new List<Card>();

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

    // Use this for initialization
    void Start ()
    {
        Transform AllCardContainer = GameObject.Find("AllCards").transform;

        NeutralContainer = AllCardContainer.Find("Neutral");
        NeutralContainer.GetComponentsInChildren(true, NeutralList);

        MageContainer = AllCardContainer.Find("Mage");
        MageContainer.GetComponentsInChildren(true, MageList);

        DiscoverSpells(Hero.Mage);
	}
	
	// Update is called once per frame
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
            if(card.hero == Hero.Neutral)
            {
                card.transform.parent = NeutralContainer;
            }
            else if(card.hero == Hero.Mage)
            {
                card.transform.parent = MageContainer;
            }
        }
        PoolOfCards.Clear();
    }

    void DiscoverSpells(Hero hero)
    {
        ResetCardPoolStatus();
        foreach (Card card in CardList(Hero.Neutral))
        {
            if (card.type == Type.Spell)
            {
                neutrals++;
                PoolOfCards.Add(card);
                card.transform.parent = this.transform;
            }
        }
        foreach (Card card in CardList(hero))
        {

            if (card.type == Type.Spell)
            {
                PoolOfCards.Add(card);
                classCards++;
                card.transform.parent = this.transform;
            }
        }

        calculator.neutrals = neutrals;
        calculator.neutralTargets = 0;
        calculator.classCards = classCards;
        calculator.classTargets = 0;
    }

    List<Card> CardList(Hero hero)
    {
        if (hero == Hero.Neutral)
            return NeutralList;
        else if (hero == Hero.Mage)
            return MageList;

        Debug.Log("hero does not exist, please ensue panic");
        return null;
    }
}
