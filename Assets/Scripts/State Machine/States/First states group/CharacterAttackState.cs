using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterAttackState : CharacterStateBase
{
    CharacterStateBase nextQueuedState;

    protected override void onEnterState()
    {
        base.onEnterState();

        nextQueuedState = charStateController.characterIdleState;
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        
        if(charType == 0)
        {
            attack();
        }
        
        onEndState(nextQueuedState);

    }


    protected override void onEndState(CharacterStateBase nextState)
    {
        charStateController.changeState_SM0(nextState);

        base.onEndState(nextState);
    }

    public void attack()
    {
        Debug.Log("attack");
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
                if(hit.collider.gameObject.tag == "Human")
                {
                    //you send your opponent to the death state
                    CharacterStateController charStateOpponent = hit.collider.gameObject.GetComponent<CharacterStateController>();
                    charStateOpponent.changeState_SM0(charStateOpponent.characterDeathState);
                    playerParentControl.score++;
                    TextMeshProUGUI scorevar = GameObject.Find("ScoreBoard" + playerParentControl.playerIndex).GetComponent<TextMeshProUGUI>();
                    scorevar.text = "Player " + (playerParentControl.playerIndex + 1) + ": " + playerParentControl.score;

                }
                else
                {
                    //you get sent to the damaged also called the stunned state
                    //Debug.Log("Stun");
                    //onEndState(charStateController.characterDamagedState);
                    nextQueuedState = charStateController.characterDamagedState;
                }
            }
        }
    }
}
