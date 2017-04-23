using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateController : MonoBehaviour {



    [SerializeField]
    CharacterIdleState _characterIdleState;
    public CharacterIdleState characterIdleState { get { return _characterIdleState; } }
    [SerializeField]
    CharacterMoveState _characterMoveState;
    public CharacterMoveState characterMoveState { get { return _characterMoveState; } }
    [SerializeField]
    CharacterAttackState _characterAttackState;
    public CharacterAttackState characterAttackState { get { return _characterAttackState; } }
    [SerializeField]
    CharacterDamagedState _characterDamagedState;
    public CharacterDamagedState characterDamagedState { get { return _characterDamagedState; } }
    [SerializeField]
    CharacterDeathState _characterDeathState; //synced state
    public CharacterDeathState characterDeathState { get { return _characterDeathState; } }

    //parallel to above states
    [SerializeField]
    CharacterSwitchState _characterSwitchState;
    public CharacterSwitchState characterSwitchState { get { return _characterSwitchState; } }

    //another parallel state, for when lightning hits
    [SerializeField]
    CharacterLightningState _characterLightningState;
    public CharacterLightningState characterLightningState { get { return _characterLightningState; } }
    

    public CharacterStateBase currentState_SM0;
    [SerializeField]
    CharacterStateBase currentState_SM1;
    [SerializeField]
    CharacterStateBase currentState_SM2;//cutscene animations e.g. lightning

    //PlayerController playerParentControl;
    PlayerController.CharacterTypes charType;
    CharacterPhysics _charPhysics;
    public CharacterPhysics charPhysics { get { return _charPhysics; } }
    [HideInInspector]
    public Animator anim;
    public GameObject knife;

    public void Init(PlayerController playerParentControl, PlayerController.CharacterTypes type)
    {
        //this.playerParentControl = playerParentControl;

        anim = GetComponentInChildren<Animator>();
        this.charType = type;

        currentState_SM0 = _characterIdleState;
        currentState_SM1 = _characterSwitchState;
        currentState_SM2 = null;

        _charPhysics = GetComponent<CharacterPhysics>();

        _characterIdleState.Init(this, playerParentControl, charType, _charPhysics);
        _characterMoveState.Init(this, playerParentControl, charType, _charPhysics);
        _characterAttackState.Init(this, playerParentControl, charType, _charPhysics);
        _characterDamagedState.Init(this, playerParentControl, charType, _charPhysics);
        _characterDeathState.Init(this, playerParentControl, charType, _charPhysics);
        _characterSwitchState.Init(this, playerParentControl, charType, _charPhysics);
        _characterLightningState.Init(this, playerParentControl, charType, _charPhysics);
    }



    // Update is called once per frame
    void Update () {

        if (currentState_SM0 != null)
        {
            //idle, move, attack, take damage or get stunned, die
            currentState_SM0.OnUpdateState();
        }

        if (currentState_SM1 != null)
        {
            //IF NOT DEAD
            //switch characters / magnetize
            currentState_SM1.OnUpdateState();
        }

        if (currentState_SM2 != null)
        {
            //play lightning animation
            currentState_SM2.OnUpdateState();
        }

    }

    public void changeState_SM0(CharacterStateBase nextState)
    {
        currentState_SM0 = nextState;
    }

    public void changeState_SM1(CharacterStateBase nextState)
    {
        currentState_SM1 = nextState;
    }

    public void changeState_SM2(CharacterStateBase nextState)
    {
        currentState_SM2 = nextState;
    }
}
