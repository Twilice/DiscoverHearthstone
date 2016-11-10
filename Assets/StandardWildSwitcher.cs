using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StandardWildSwitcher : MonoBehaviour {

    bool usingStandard = true;
    Text textbox;
    Cardpool cardpool;

    // Use this for initialization
    void Start () {
        cardpool = GameObject.Find("CardPool").GetComponent<Cardpool>();
        textbox = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PointerDown()
    {
        usingStandard = !usingStandard;
        if (usingStandard)
            textbox.text = "Standard";
        else
            textbox.text = "Wild";

        cardpool.standard = usingStandard;
    }
    
}
