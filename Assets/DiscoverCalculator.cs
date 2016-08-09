using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DiscoverCalculator : MonoBehaviour {


    [Header("Testvalues")]
    public float neutrals = 10;
    public float classCards = 5;
    public float neutralTargets = 2;
    public float classTargets = 1;

    [Header("Constants")]
    public float classBonus = 4;
    public float neutralBonus = 1;
    public float cardsDrawn = 3;

    private Text text;

    void Start ()
    {
        text = GetComponent<Text>();
	}
	
	void Update ()
    {
        CardDrawChance(neutrals, classCards, neutralTargets, classTargets);

    }

    void UpdatePercentage()
    {
        CardDrawChance(neutrals, classCards, neutralTargets, classTargets);
    }

    void CardDrawChance(float neutrals, float classCards, float neutralTargets, float classTargets)
    {
        text.text =  (Mathf.Round((1 - CardDrawChance_inner(neutralBonus * neutrals, classBonus * classCards, neutralBonus * neutralTargets, classBonus * classTargets, cardsDrawn)) * 1000) / 10).ToString();
    }

    float CardDrawChance_inner(float neutrals, float classCards, float neutralTargets, float classTargets, float draws)
    {
        if (draws == 0)
            return 1;
        else if (classTargets > classCards)
            return 1;
        else if (neutralTargets > neutrals)
            return 1;
        else if (classTargets + neutralTargets >= neutrals + classCards)
            return 0;
        else
            return (classCards - classTargets) / (neutrals + classCards) * CardDrawChance_inner(neutrals, classCards - classBonus, neutralTargets, classTargets, draws - 1) +
            (neutrals - neutralTargets) / (neutrals + classCards) * CardDrawChance_inner(neutrals - neutralBonus, classCards, neutralTargets, classTargets, draws - 1);
    }

}

/*
#include <iostream>
using namespace std;
#define classBonus 4
#define neutralBonus 1
#define cardsDrawn 3

// Version 1.0
/*float CardDrawChance_inner(float pool, float misses, float draws)
{
	if (draws == 0)
		return 1;
	else if (misses == 0)
		return 0;
	else
		return misses/pool * CardDrawChance_inner(pool-1, misses-1, draws-1);
}

float CardDrawChance(float pool, float targets)
{
	return std::round((1 - CardDrawChance_inner(pool, pool-targets, 3))*1000)/10;
}*/

    /*
// Version 2.0
float CardDrawChance_inner(float neutrals, float classCards, float neutralTargets, float classTargets, float draws)
{
    if (draws == 0)
        return 1;
    else if (classTargets > classCards)
        return 1;
    else if (neutralTargets > neutrals)
        return 1;
    else if (classTargets + neutralTargets >= neutrals + classCards)
        return 0;
    else
        return (classCards - classTargets) / (neutrals + classCards) * CardDrawChance_inner(neutrals, classCards - classBonus, neutralTargets, classTargets, draws - 1) +
        (neutrals - neutralTargets) / (neutrals + classCards) * CardDrawChance_inner(neutrals - neutralBonus, classCards, neutralTargets, classTargets, draws - 1);
}


float CardDrawChance(float neutrals, float classCards, float neutralTargets, float classTargets)
{
    return std::round((1 - CardDrawChance_inner(neutralBonus * neutrals, classBonus * classCards, neutralBonus * neutralTargets, classBonus * classTargets, cardsDrawn)) * 1000) / 10;
}

int main()
{
    int neutrals = 0;
    int classCards = 0;
    int choise = 0;
    float value = 0;
    int soughtNeutrals = 0;
    int soughtClassCards = 0;

    cout << "Exit by typing a negative number (eg. -1)" << endl;

    while (neutrals != -1 && classCards != -1 && choise != -1)
    {
        cout << endl << "Number of neutral cards: ";
        cin >> neutrals;
        if (neutrals < 0)
            break;
        cout << endl << "Number of class cards: ";
        cin >> classCards;
        if (classCards < 0)
            break;

        choise = 1;
        while (choise > 0)
        {
            cout << endl << endl
                << "What do you want to calculate?" << endl
                << "1, Chance for a specifik neutral." << endl
                << "2, Chance for a specifik class card." << endl
                << "3, Chance for custom situation." << endl
                << "0, Restart" << endl;

            cin >> choise;

            if (choise == 1)
            {
                cout << "Chance: " << CardDrawChance((float)neutrals, (float)classCards, 1, 0) << "%" << endl;
            }
            else if (choise == 2)
            {
                cout << "Chance: " << CardDrawChance((float)neutrals, (float)classCards, 0, 1) << "%" << endl;
            }
            else if (choise == 3)
            {
                cout << endl << "Number of sought neutral cards: ";
                cin >> soughtNeutrals;
                if (soughtNeutrals < 0)
                {
                    choise = -1;
                    break;
                }
                cout << endl << "Number of sought class cards: ";
                cin >> soughtClassCards;
                if (soughtClassCards < 0)
                {
                    choise = -1;
                    break;
                }
                cout << "Chance: " << CardDrawChance((float)neutrals, (float)classCards, (float)soughtNeutrals, (float)soughtClassCards) << "%" << endl;
            }

        }
    }

    return 0;
}
*/