using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    public float speed =  10.0F;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
     float xmove = Input.GetAxis("Horizontal") * speed;
        float ymove = Input.GetAxis("Vertical") * speed;
        xmove *= Time.deltaTime;
        ymove *= Time.deltaTime;
        transform.Translate(xmove, ymove, 0);
      
    }
}
