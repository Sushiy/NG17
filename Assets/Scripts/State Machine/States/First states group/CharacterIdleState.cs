using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : CharacterStateBase {

    protected override void onEnterState()
    {
        base.onEnterState();

    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();

        //TODO: Play idle animations, text bubbles etc.

        if(
            Mathf.Abs(playerParentControl.charInputs[(int)charType].moveAxis.x) > 0.0f ||
            Mathf.Abs(playerParentControl.charInputs[(int)charType].moveAxis.y) > 0.0f
            )
        {
            onEndState(charStateController.characterMoveState);
        }
    }

    protected override void onEndState(CharacterStateBase nextState)
    {
       
        charStateController.changeState_SM0(nextState);

        base.onEndState(nextState);
    }
}
