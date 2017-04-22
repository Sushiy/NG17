using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateBase : MonoBehaviour {
    [SerializeField]
    bool debugLines = false;

    protected CharacterStateController charStateController;
    protected PlayerController playerParentControl;
    protected PlayerController.CharacterTypes charType;
    protected CharacterPhysics charPhysics;

    bool hasStarted = false;

    public virtual void Init(CharacterStateController charStateController, PlayerController playerParentControl, PlayerController.CharacterTypes charType, CharacterPhysics charPhysics)
    {
        this.charStateController = charStateController;
        this.playerParentControl = playerParentControl;
        this.charType = charType;
        this.charPhysics = charPhysics;

        if (debugLines)
            Debug.Log("Initializing State " + this.ToString() + "; " + charType.ToString());
    }


    protected virtual void onEnterState()
    {
        hasStarted = true;
        if (debugLines)
            Debug.Log("Entered State " + this.ToString() + "; " + charType.ToString());
    }

    public virtual void OnUpdateState()
    {
        if (!hasStarted)
        {
            onEnterState();
        }
    }

    protected virtual void onEndState(CharacterStateBase nextState)
    {
        hasStarted = false;
        if (debugLines)
            Debug.Log("Ended State " + this.ToString() + "; " + charType.ToString());
    }


}
