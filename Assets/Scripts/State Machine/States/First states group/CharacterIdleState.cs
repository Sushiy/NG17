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

        bool attacking = playerParentControl.charInputs[(int)charType].attack;
        if (attacking)
        {
            onEndState(charStateController.characterAttackState);
        }

        if (playerParentControl.charInputs[(int)charType].moveAxis.sqrMagnitude > 0.01f)
        {
            Debug.Log("go to move");
            onEndState(charStateController.characterMoveState);
        }
    }

    protected override void onEndState(CharacterStateBase nextState)
    {
       
        charStateController.changeState_SM0(nextState);

        base.onEndState(nextState);
    }
}
