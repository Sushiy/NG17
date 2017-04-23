using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeathState : CharacterStateBase
{

    protected override void onEnterState()
    {
        base.onEnterState();

        if(charType == 0)
            playerParentControl.playerAudio.PlayDeathStinger();
        
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();

        playerParentControl.Respawn(true);
        

        

    }


    protected override void onEndState(CharacterStateBase nextState)
    {
        charStateController.changeState_SM0(nextState);

        base.onEndState(nextState);
    }

    
}
