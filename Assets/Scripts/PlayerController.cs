using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerIndex = 0;

    public GameObject realPlayerPrefab, ghostPlayerPrefab;

    [HideInInspector]
    public float leftStickX, leftStickY, rightStickX, rightStickY, triggerAxis;

    private bool isXbox360Ctrl = true;

    // Use this for initialization
    void Awake ()
    {
        GameObject.Instantiate(realPlayerPrefab, transform.position, transform.rotation, transform);
        GameObject.Instantiate(ghostPlayerPrefab, transform.position, transform.rotation, transform);
        string joystickName = Input.GetJoystickNames()[playerIndex];
        
        switch(joystickName)
        {
            case "Controller (XBOX 360 For Windows)":
                isXbox360Ctrl = true;
                break;
            default:
                isXbox360Ctrl = false;
                break;
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
        //X and Y axis are always the first 2
        leftStickX = Input.GetAxis("XAxis_" + playerIndex);
        leftStickY = Input.GetAxis("YAxis_" + playerIndex);

        if(isXbox360Ctrl)
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
