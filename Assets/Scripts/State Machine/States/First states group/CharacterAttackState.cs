using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackState : CharacterStateBase
{


    protected override void onEnterState()
    {
        base.onEnterState();



    }

    public override void OnUpdateState()
    {
        base.onEnterState();

        attack();
        onEndState(charStateController.characterIdleState);

    }


    protected override void onEndState(CharacterStateBase nextState)
    {
        charStateController.changeState_SM0(nextState);

        base.onEndState(nextState);
    }

    public void attack()
    {
        RaycastHit hit;
        Vector3 position = transform.position;
        Vector3 direction = transform.forward;
        Debug.DrawLine(position, position + direction, Color.red);
        if (Physics.SphereCast(position, playerParentControl.charSettings.attackSphereCastRadius, direction, out hit, playerParentControl.charSettings.attackLength))
        {
            int layer = hit.collider.gameObject.layer;
            if (layer >= 8 && layer <= 11 && (layer != (8 + playerParentControl.playerIndex)))
            {
                Debug.Log("we hit " + hit.collider.gameObject.layer.ToString());
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
