using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public bool attacking = false;

    PlayerController controller;


    // Use this for initialization
    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float buttonPress = Input.GetAxis("Axis3_" + controller.playerIndex);
        if ( buttonPress < -0.3)
        {
            attacking = true;
        }

        if (attacking)
        {
            RaycastHit hit;
            Vector3 position = transform.position;
            Vector3 direction = transform.forward;
            Debug.DrawLine(position, position + direction, Color.red);
            if (Physics.SphereCast(position+direction, 0.5f, direction, out hit, 0.005f))
            {
                if (hit.collider.gameObject.layer == 9)
                {
                    Debug.Log("we hit something");
                    Destroy(hit.collider.gameObject);
                }
            }

            attacking = false;
        }
    }
}
