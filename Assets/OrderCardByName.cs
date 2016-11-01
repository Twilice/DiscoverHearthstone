using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class OrderCardByName : MonoBehaviour {
    
    [InspectorButton("OnButtonClicked")]
    public bool sortByName = false;

    private void OnButtonClicked()
    {
        Card[] childs = GetComponentsInChildren<Card>();
        List<Card> list = new List<Card>(childs);
        list.Sort((Card x, Card y) => { return x.name.CompareTo(y.name); });

        foreach (Card c in list)
        {
            c.transform.SetParent(null, false);
            c.transform.SetParent(transform, false);
        }
	}
}
