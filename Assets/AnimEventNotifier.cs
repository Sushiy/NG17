using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventNotifier : MonoBehaviour
{
    CharacterStateController csc;
    private void Start()
    {
       csc = GetComponentInParent<CharacterStateController>();
    }

    public void AttackStarts()
    {
        (csc.currentState_SM0 as CharacterAttackState).isAttacking = true;
    }

    public void AttackEnds()
    {

        (csc.currentState_SM0 as CharacterAttackState).EndAttack();
    }
}
