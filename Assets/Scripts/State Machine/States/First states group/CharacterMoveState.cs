using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveState : CharacterStateBase
{

    protected override void onEnterState()
    {
        base.onEnterState();

    }

    public override void OnUpdateState()
    {
        base.onEnterState();

        Vector2 moveAxis = playerParentControl.charInputs[(int)charType].moveAxis;
        float charSpeed = playerParentControl.charSettings.moveSpeed;

        move(moveAxis, charSpeed);


        bool attacking = playerParentControl.charInputs[(int)charType].attack;
        if (attacking)
        {
            onEndState(charStateController.characterAttackState);
        }

        if (
            Mathf.Abs(playerParentControl.charInputs[(int)charType].moveAxis.x) == 0.0f ||
            Mathf.Abs(playerParentControl.charInputs[(int)charType].moveAxis.y) == 0.0f
            )
        {
            onEndState(charStateController.characterIdleState);
        }

        
    }

    void move(Vector2 moveAxis, float speed)
    {
        float xMove = moveAxis.x * speed * Time.deltaTime;
        float zMove = -moveAxis.y * speed * Time.deltaTime;

        charPhysics.transform.Translate(xMove, 0, zMove, Space.World);
        charPhysics.transform.LookAt(transform.position + new Vector3(xMove, 0, zMove) * 10.0f);
    }

    protected override void onEndState(CharacterStateBase nextState)
    {
        charStateController.changeState_SM0(nextState);

        base.onEndState(nextState);
    }
}
