using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    bool attacking = false;

    PlayerController controller;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Axis1_" + controller.playerIndex)<-0.3)
        {
            attacking = true;
        }

        if (attacking)
        {

        }
    }
}
