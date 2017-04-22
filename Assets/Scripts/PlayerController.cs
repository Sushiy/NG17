using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerIndex = 0;

    public GameObject characterPrefab;

    public Material humanMat;
    public Material ghostMat;

    GameObject realPlayer, ghostPlayer;
    CharacterStateController _realPlayerSC, _ghostPlayerSC;
    public CharacterStateController realPlayerSC { get { return _realPlayerSC; } }
    public CharacterStateController ghostPlayerSC { get { return _ghostPlayerSC; } }

  

    public float leftStickX, leftStickY, rightStickX, rightStickY, triggerAxis;

    public bool isXboxCtrl = true;

    public enum CharacterTypes { human = 0, ghost = 1}

    public struct InputSet
    {
        public Vector2 moveAxis;
        public float switchValue;
        public bool attack;
        public bool attackOld;
    }
    InputSet[] _charInputs = new InputSet[2];
    public InputSet[] charInputs { get { return _charInputs; } }

    [System.Serializable]
    public struct CharacterSettings
    {
        public float moveSpeed;
        public float attackSphereCastRadius;
        public float attackLength;
        public float stunTime;
        public float gravityPower;
        public AnimationCurve gravityCurve;
        public float maxGravityDistance;
    }
    [SerializeField]
    CharacterSettings _charSettings;
    public CharacterSettings charSettings { get { return _charSettings; } }

    // Use this for initialization
    void Awake ()
    {
        /*
        realPlayer = GameObject.Instantiate(characterPrefab, transform.position, transform.rotation, transform);
        ghostPlayer = GameObject.Instantiate(characterPrefab, transform.position, transform.rotation, transform);
        realPlayerSC = realPlayer.GetComponent<CharacterStateController>();
        ghostPlayerSC = ghostPlayer.GetComponent<CharacterStateController>();
        realPlayerSC.Init(this, CharacterTypes.human);
        ghostPlayerSC.Init(this, CharacterTypes.ghost);
        */
        realPlayer = GameObject.Instantiate(characterPrefab, transform.position, transform.rotation, transform);
        ghostPlayer = GameObject.Instantiate(characterPrefab, transform.position, transform.rotation, transform);
        _realPlayerSC = realPlayer.GetComponent<CharacterStateController>();
        _ghostPlayerSC = ghostPlayer.GetComponent<CharacterStateController>();
        _realPlayerSC.Init(this, CharacterTypes.human);
        _ghostPlayerSC.Init(this, CharacterTypes.ghost);
        //realPlayer = GameObject.Instantiate(characterPrefab, transform.position, transform.rotation, transform);


        if (Input.GetJoystickNames().Length <= playerIndex)
        {
            gameObject.SetActive(false);
            return;
        }

        string joystickName = Input.GetJoystickNames()[playerIndex];

        realPlayer.layer = this.gameObject.layer;
        realPlayer.tag = "Human";
        ghostPlayer.tag = "Ghost";
        ghostPlayer.layer = this.gameObject.layer;
        
        switch(joystickName)
        {
            case "Controller (XBOX 360 For Windows)":
                isXboxCtrl = true;
                break;
            case "Controller (Xbox One For Windows)":
                isXboxCtrl = true;
                break;
            default:
                isXboxCtrl = false;
                break;
        }

        Debug.Log(joystickName);

	}
	
	// Update is called once per frame
	void Update ()
    {
        //X and Y axis are always the first 2
        leftStickX = Input.GetAxis("Axis1_" + playerIndex);
        leftStickY = Input.GetAxis("Axis2_" + playerIndex);

        if(isXboxCtrl)
        {
            //If this controller is xbox360, the trigger axis is axis3 and rightstick is 4,5
            triggerAxis = Input.GetAxis("Axis3_" + playerIndex);
            rightStickX = Input.GetAxis("Axis4_" + playerIndex);
            rightStickY = Input.GetAxis("Axis5_" + playerIndex);
        }
        else
        {
            //Otherwise the trigger axis is axis5 and rightstick is 3,4
            rightStickX = Input.GetAxis("Axis3_" + playerIndex);
            rightStickY = Input.GetAxis("Axis4_" + playerIndex);
            triggerAxis = Input.GetAxis("Axis5_" + playerIndex);
        }

        _charInputs[(int)CharacterTypes.ghost].moveAxis = new Vector3(leftStickX, leftStickY);
        //if (!_charInputs[(int)CharacterTypes.ghost].attackOld && Mathf.Abs(triggerAxis) > 0.3f)
        //{
        //    
        //    _charInputs[(int)CharacterTypes.ghost].attack = true;
        //}
        //else _charInputs[(int)CharacterTypes.ghost].attack = false;

        //_charInputs[(int)CharacterTypes.ghost].attackOld = Mathf.Abs(triggerAxis) < 0.3f ? false : true;
        _charInputs[(int)CharacterTypes.ghost].switchValue = 0; 

        _charInputs[(int)CharacterTypes.human].moveAxis = new Vector3(rightStickX, rightStickY);
        if (!_charInputs[(int)CharacterTypes.human].attackOld && Mathf.Abs(triggerAxis) > 0.3f)
        {
            
            _charInputs[(int)CharacterTypes.human].attack = true;
        }
        else _charInputs[(int)CharacterTypes.human].attack = false;
        _charInputs[(int)CharacterTypes.human].attackOld = Mathf.Abs(triggerAxis) < 0.3f ? false : true;
        _charInputs[(int)CharacterTypes.human].switchValue = 0;

        if (LightningManager.instance.isLightningStriking)
        {
            ghostPlayer.GetComponentInChildren<SkinnedMeshRenderer>().material = ghostMat;
        }
        else
        {
            ghostPlayer.GetComponentInChildren<SkinnedMeshRenderer>().material = humanMat;
        }
    }

    public void respawn()
    {
        Destroy(realPlayer);
        Destroy(ghostPlayer);
        int x = Random.Range(1, 5);
        Vector3 spawn;
        switch (x)
        {
            case 1: spawn = new Vector3(14f, 0, -9.9f);
                break;
            case 2: spawn = new Vector3(1.5f, 0, 9.14f);
                break;
            case 3: spawn = new Vector3(-14.27f, 0, 10.18f);
                break;
            case 4: spawn = new Vector3(-14.27f, 0, -9.94f);
                break;
            default: spawn = new Vector3(0,0,0);
                break;
        }

        realPlayer = GameObject.Instantiate(characterPrefab, spawn, transform.rotation, transform);
        ghostPlayer = GameObject.Instantiate(characterPrefab, spawn, transform.rotation, transform);
        _realPlayerSC = realPlayer.GetComponent<CharacterStateController>();
        _ghostPlayerSC = ghostPlayer.GetComponent<CharacterStateController>();
        _realPlayerSC.Init(this, PlayerController.CharacterTypes.human);
        _ghostPlayerSC.Init(this, PlayerController.CharacterTypes.ghost);


        realPlayer.layer = this.gameObject.layer;
        ghostPlayer.layer = this.gameObject.layer;

        realPlayer.tag = "Human";
    }
}
