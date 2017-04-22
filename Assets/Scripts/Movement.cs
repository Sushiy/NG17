using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed =  10.0F;

    public bool isControlledByLeftStick = true;

    PlayerController controller;

	// Use this for initialization
	void Start ()
    {
        controller = GetComponentInParent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float axisX, axisY;
        if (isControlledByLeftStick)
        {
            axisX = controller.leftStickX;
            axisY = controller.leftStickY;
        }
        else
        {
            axisX = controller.rightStickX;
            axisY = controller.rightStickY;
        }

        float xMove = axisX * speed * Time.deltaTime;
        float zMove = -axisY * speed * Time.deltaTime;

        transform.Translate(xMove, 0, zMove, Space.World);
        transform.LookAt(transform.position + new Vector3(xMove, 0, zMove) * 10.0f);
      
    }
}
