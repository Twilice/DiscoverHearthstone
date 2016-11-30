using UnityEngine;
using System.Collections;

public class Button_MoveObject : MonoBehaviour {

    public RectTransform target;
    public Vector2 velocity;
    public Vector2 acceleration;

    private bool buttonPressed;
	void Start () {
	
	}
	
	void Update () {
        if (buttonPressed)
        {
            velocity += acceleration * Time.deltaTime;
            target.anchoredPosition += velocity * Time.deltaTime;
        }
	}

    public void PointerDown()
    {
        if (buttonPressed == false)
        {
            buttonPressed = true;
            velocity = acceleration * 2;
        }
    }

    public void PointerUp()
    {
        if (buttonPressed == true)
        {
            buttonPressed = false;
            velocity = Vector2.zero;
        }
    }
}
