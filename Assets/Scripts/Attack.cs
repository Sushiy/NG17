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

        attack();

        
    }
    /// <summary>
    /// Method that checks if you are pressing the attack button and then casting a sphere to check for collision
    /// </summary>
    public void attack()
    {
        if (attacking)
        {
            RaycastHit hit;
            Vector3 position = transform.position;
            Vector3 direction = transform.forward;
            Debug.DrawLine(position, position + direction, Color.red);
            if (Physics.SphereCast(position, 0.5f, direction, out hit, 0.5f))
            {
                int layer = hit.collider.gameObject.layer;
                if (layer >= 8 && layer <= 11 && (layer != (8 + controller.playerIndex)))
                {
                    Debug.Log("we hit " + hit.collider.gameObject.layer.ToString());
                    Destroy(hit.collider.gameObject);
                }
            }

            attacking = false;
        }
    }
}
