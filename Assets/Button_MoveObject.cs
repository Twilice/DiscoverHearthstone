using UnityEngine;
using System.Collections;

public class Button_MoveObject : MonoBehaviour {

    public Transform target;
    public Vector2 velocity;

    private bool buttonPressed;
	void Start () {
	
	}
	
	void Update () {
        if (buttonPressed)
        {
            target.position += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime;
        }
	}

    public void PointerDown()
    {
        buttonPressed = true;
    }

    public void PointerUp()
    {
        buttonPressed = false;
    }
}
