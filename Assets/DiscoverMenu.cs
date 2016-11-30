using UnityEngine;
using System.Collections;
using System;

public class DiscoverMenu : MonoBehaviour {

	private Cardpool cardpool;

	private Action<Hero> discover;

    // cardpool component must be initialized before DiscoverFunction is set, thus awake instead of start
    void Awake()
    {
		cardpool = GameObject.Find("CardPool").GetComponent<Cardpool>();
		//discover = cardpool.DiscoverSpells;
    }

    void Update()
    {

    }

	public void Reset()
	{
        //unhide discover menu
        cardpool.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
		gameObject.SetActive(true);
	}

	public void DiscoverSpell()
	{
		discover = cardpool.DiscoverSpells;
		//hide discover menu
	}

    public void Discover3Mana()
    {
        discover = cardpool.Discover3Mana;
        //hide discover menu
    }

    public void Discover1Mana()
    {
        discover = cardpool.Discover1Mana;
        //hide discover menu
    }
    public void DiscoverDeathrattle()
    {
        discover = cardpool.DiscoverDeathrattle;
        //hide discover menu
    }
    public void DiscoverTaunt()
    {
        discover = cardpool.DiscoverTaunt;
        //hide discover menu
    }

    public void DiscoverOverload()
    {
        discover = cardpool.DiscoverOverload;
        //hide discover menu
    }
    public void DiscoverMinion()
    {
        discover = cardpool.DiscoverMinion;
        //hide discover menu
    }

    public void DiscoverDragon()
    {
        discover = cardpool.DiscoverDragon;
        //hide discover menu
    }
    public void DiscoverBeast()
    {
        discover = cardpool.DiscoverBeast;
        //hide discover menu
    }
    public void DiscoverMech()
    {
        discover = cardpool.DiscoverMech;
        //hide discover menu
    }

    public void DiscoverKabal()
    {
        discover = cardpool.DiscoverKabal;
    }

    public void DiscoverGoons()
    {
        discover = cardpool.DiscoverGoons;
    }

    public void DiscoverLotus()
    {
        discover = cardpool.DiscoverLotus;
    }

    public void ChooseDruid()
	{
		discover(Hero.Druid);
		gameObject.SetActive(false);
	}
	public void ChooseHunter()
	{
		discover(Hero.Hunter);
		gameObject.SetActive(false);
	}
	public void ChooseMage()
	{
		discover(Hero.Mage);
		gameObject.SetActive(false);
	}
	public void ChoosePaladin()
	{
		discover(Hero.Paladin);
		gameObject.SetActive(false);
	}
	public void ChoosePriest()
	{
		discover(Hero.Priest);
		gameObject.SetActive(false);
	}
	public void ChooseRogue()
	{
		discover(Hero.Rogue);
		gameObject.SetActive(false);
	}
	public void ChooseShaman()
	{
		discover(Hero.Shaman);
		gameObject.SetActive(false);
	}
	public void ChooseWarlock()
	{
		discover(Hero.Warlock);
		gameObject.SetActive(false);
	}
	public void ChooseWarrior()
	{
		discover(Hero.Warrior);
		gameObject.SetActive(false);
	}
}
