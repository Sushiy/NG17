using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeathState : CharacterStateBase
{

    protected override void onEnterState()
    {
        base.onEnterState();


    }

    public override void OnUpdateState()
    {
        base.onEnterState();

        Destroy(playerParentControl.ghostPlayer.gameObject);
        Destroy(playerParentControl.realPlayer.gameObject);

    }


    protected override void onEndState(CharacterStateBase nextState)
    {
        charStateController.changeState_SM0(nextState);

        base.onEndState(nextState);
    }
}
