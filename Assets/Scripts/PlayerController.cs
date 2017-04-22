using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerIndex = 0;

    public GameObject realPlayerPrefab, ghostPlayerPrefab;

    [HideInInspector]
    public float leftStickX, leftStickY, rightStickX, rightStickY, triggerAxis;

    public bool isXboxCtrl = true;

    // Use this for initialization
    void Awake ()
    {
        GameObject.Instantiate(realPlayerPrefab, transform.position, transform.rotation, transform);
        GameObject.Instantiate(ghostPlayerPrefab, transform.position, transform.rotation, transform);
        string joystickName = Input.GetJoystickNames()[playerIndex];
        
        switch(joystickName)
        {
            case "Controller (XBOX 360 For Windows)":
                isXboxCtrl = true;
                break;
            case "Controller (Xbox One For Windows)":
                isXboxCtrl = true;
                break;
            default:
                isXboxCtrl = false;
                break;
        }

        Debug.Log(joystickName);

	}
	
	// Update is called once per frame
	void Update ()
    {
        //X and Y axis are always the first 2
        leftStickX = Input.GetAxis("Axis1_" + playerIndex);
        leftStickY = Input.GetAxis("Axis2_" + playerIndex);

        if(isXboxCtrl)
        {
            //If this controller is xbox360, the trigger axis is axis3 and rightstick is 4,5
            triggerAxis = Input.GetAxis("Axis3_" + playerIndex);
            rightStickX = Input.GetAxis("Axis4_" + playerIndex);
            rightStickY = Input.GetAxis("Axis5_" + playerIndex);
        }
        else
        {
            //Otherwise the trigger axis is axis5 and rightstick is 3,4
            rightStickX = Input.GetAxis("Axis3_" + playerIndex);
            rightStickY = Input.GetAxis("Axis4_" + playerIndex);
            triggerAxis = Input.GetAxis("Axis5_" + playerIndex);
        }

    }
}
