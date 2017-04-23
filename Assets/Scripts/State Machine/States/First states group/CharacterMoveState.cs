using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveState : CharacterStateBase
{

    protected override void onEnterState()
    {
        base.onEnterState();
        charStateController.anim.SetBool("bWalking", true);

    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();

        Vector2 moveAxis = playerParentControl.charInputs[(int)charType].moveAxis;
        float charSpeed = playerParentControl.charSettings.moveSpeed;

        move(moveAxis, charSpeed);



        bool attacking = playerParentControl.charInputs[(int)charType].attack;
        if (attacking)
        {
            onEndState(charStateController.characterAttackState);
        }

        if (playerParentControl.charInputs[(int)charType].moveAxis.sqrMagnitude <= 0.01f)
        {
            onEndState(charStateController.characterIdleState);
        }

        
    }

    void move(Vector2 moveAxis, float speed)
    {
        float xMove = moveAxis.x * speed;
        float zMove = -moveAxis.y * speed;
        Vector3 lookPositon = new Vector3(xMove, 0, zMove);

        Vector3 dir = (lookPositon).normalized;
        charPhysics.accumulateLookTarget(dir);
        charPhysics.accumulateTargetDirMove(dir);
        charPhysics.LookWhereYoureGoing();

        charPhysics.accumulateSpeedMove(speed * Time.deltaTime);

        
    }

    protected override void onEndState(CharacterStateBase nextState)
    {
        charStateController.changeState_SM0(nextState);
        charStateController.anim.SetBool("bWalking", false);

        base.onEndState(nextState);
    }
}
