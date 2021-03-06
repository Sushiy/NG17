﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamagedState : CharacterStateBase
{

    float counter;
    protected override void onEnterState()
    {
        base.onEnterState();

        counter = 0;

        playerParentControl.playerAudio.PlayStunStinger();


    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        //base.onEnterState();

      
        //Stunned for 2seconds
        counter += Time.deltaTime;


        if (counter >= playerParentControl.charSettings.stunTime)
        {
            onEndState(charStateController.characterIdleState);
        }

    }


    protected override void onEndState(CharacterStateBase nextState)
    {
        charStateController.changeState_SM0(nextState);

        base.onEndState(nextState);
    }
}
