using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitchState : CharacterStateBase
{
    CharacterPhysics otherCharPhysics;
    bool initialized = false;

    public override void Init(CharacterStateController charStateController, PlayerController playerParentControl, PlayerController.CharacterTypes charType, CharacterPhysics charPhysics)
    {
        base.Init(charStateController, playerParentControl, charType, charPhysics);

        StartCoroutine(Init2());
    }

    IEnumerator Init2()
    {
        yield return new WaitForEndOfFrame();

        if (charType == PlayerController.CharacterTypes.human)
        {
            otherCharPhysics = playerParentControl.ghostPlayerSC.charPhysics;

        }
        else if (charType == PlayerController.CharacterTypes.ghost)
        {
            otherCharPhysics = playerParentControl.realPlayerSC.charPhysics;

        }

        initialized = true;
    }

    protected override void onEnterState()
    {
        base.onEnterState();

    }

    public override void OnUpdateState()
    {
        if (!initialized)
        {
            return;
        }
        base.onEnterState();

        
        

        GravityPull();



    }

    void GravityPull()
    {

        float gravitySpeed = playerParentControl.charSettings.gravityPower;
        float distance = Vector3.Distance(charPhysics.transform.position, otherCharPhysics.transform.position);
        if (distance < 0.05f)
            return;
        float gravityCurve = playerParentControl.charSettings.gravityCurve.Evaluate(distance / playerParentControl.charSettings.maxGravityDistance);
        float step =  gravityCurve * gravityCurve * gravitySpeed;

        //charPhysics.transform.position = Vector3.MoveTowards(charPhysics.transform.position, otherCharPhysics.transform.position, step);
        Vector3 dir = (otherCharPhysics.transform.position - charPhysics.transform.position).normalized;
        charPhysics.accumulateLookTarget(dir);
        charPhysics.accumulateTargetDir(dir);

        //if (step > playerParentControl.charSettings.maxspeed)
        //    step = playerParentControl.charSettings.maxspeed;

        charPhysics.accumulateSpeed(step * Time.deltaTime);
    }

    protected override void onEndState(CharacterStateBase nextState)
    {
        charStateController.changeState_SM0(nextState);

        base.onEndState(nextState);
    }
}
