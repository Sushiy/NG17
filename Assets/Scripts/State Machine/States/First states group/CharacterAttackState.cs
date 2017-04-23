using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterAttackState : CharacterStateBase
{
    CharacterStateBase nextQueuedState;

    public bool isAttacking = false;
    public LayerMask layers;
    public void EndAttack()
    {
        onEndState(nextQueuedState);
    }

    protected override void onEnterState()
    {
        base.onEnterState();

        nextQueuedState = charStateController.characterIdleState;
        charStateController.anim.SetTrigger("tAttack");
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        
        if(charType == 0 && isAttacking)
        {
            attack();
        }

    }


    protected override void onEndState(CharacterStateBase nextState)
    {
        charStateController.changeState_SM0(nextState);
        charStateController.knife.SetActive(false); 
        base.onEndState(nextState);
    }

    public void attack()
    {
        RaycastHit[] hitArray;
        Vector3 position = transform.position;
        Vector3 direction = transform.forward;
        Debug.DrawLine(position, position + direction, Color.red);
        charStateController.knife.SetActive(true);

        Debug.Log("attack");
        hitArray = Physics.SphereCastAll(position, playerParentControl.charSettings.attackSphereCastRadius, direction, playerParentControl.charSettings.attackLength, layers);
        if(hitArray.Length > 0)
        {
            foreach(RaycastHit hit in hitArray)
            {
                if(hit.collider.gameObject.layer != playerParentControl.gameObject.layer)
                {
                    Debug.Log("we hit " + hit.collider.gameObject.layer.ToString() + hit.collider.gameObject.tag);
                    if (hit.collider.gameObject.tag == "Human")
                    {
                        //you send your opponent to the death state
                        CharacterStateController charStateOpponent = hit.collider.gameObject.GetComponent<CharacterStateController>();
                        charStateOpponent.changeState_SM0(charStateOpponent.characterDeathState);
                        playerParentControl.score++;
                        TextMeshProUGUI scorevar = GameObject.Find("ScoreBoard" + playerParentControl.playerIndex).GetComponent<TextMeshProUGUI>();
                        scorevar.text = "Player " + (playerParentControl.playerIndex + 1) + ": " + playerParentControl.score;
                        break;
                    }
                    else
                    {
                        //you get sent to the damaged also called the stunned state
                        //Debug.Log("Stun");
                        //onEndState(charStateController.characterDamagedState);
                        charStateController.anim.SetTrigger("tStunned");
                        nextQueuedState = charStateController.characterDamagedState;
                    }

                }
            }
        }
        isAttacking = false;

    }
}
