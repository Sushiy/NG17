using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour {

    bool isMerged = false;
    PlayerController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponentInParent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isMerged)
        {
            merger();
        }
        else
        {
            
        }
	}

    void merger()
    {

    }
}
