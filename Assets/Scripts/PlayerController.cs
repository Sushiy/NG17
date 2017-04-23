using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [HideInInspector]
    public float leftStickX, leftStickY, rightStickX, rightStickY, triggerAxis;

    public bool isXboxCtrl = true;

    public int score = 0;

    public enum CharacterTypes { human = 0, ghost = 1}

    public GameObject SpawnpointParent;

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
        
        public float attackSphereCastRadius;
        public float attackLength;
        public float stunTime;
        public float respawnDelay;
        
        public float moveSpeed;
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
        respawn();
        if (Input.GetJoystickNames().Length <= playerIndex)
        {
            gameObject.SetActive(false);
            return;
        }

        string joystickName = Input.GetJoystickNames()[playerIndex];


        switch (joystickName)
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
        _charInputs[(int)CharacterTypes.ghost].switchValue = 0; 

        _charInputs[(int)CharacterTypes.human].moveAxis = new Vector3(rightStickX, rightStickY);
        if (!_charInputs[(int)CharacterTypes.human].attackOld && Mathf.Abs(triggerAxis) > 0.3f)
        {
            
            _charInputs[(int)CharacterTypes.human].attack = true;
        }
        else _charInputs[(int)CharacterTypes.human].attack = false;
        _charInputs[(int)CharacterTypes.human].attackOld = Mathf.Abs(triggerAxis) < 0.3f ? false : true;
        _charInputs[(int)CharacterTypes.human].switchValue = 0;

        if(ghostPlayer!= null)
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
        Transform[] spawns = SpawnpointParent.GetComponentsInChildren<Transform>();
        int index = Random.Range(0, spawns.Length);
        Debug.Log(index + "/" + spawns.Length);

        if (realPlayer != null)
        {
            Vector3 oldPosition = realPlayer.transform.position;
            Destroy(realPlayer);
            Destroy(ghostPlayer);

            while (Vector3.Distance(spawns[index].position, oldPosition) < 4)
            {
                index = Random.Range(0, spawns.Length);
                Debug.Log(index + "/" + spawns.Length);
            }
        }

        StartCoroutine(DelayedSpawn(spawns[index].position));
    }

    IEnumerator DelayedSpawn (Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(charSettings.respawnDelay);

        Spawn(spawnPosition);
    }

    void Spawn(Vector3 position)
    {
        realPlayer = GameObject.Instantiate(characterPrefab, position, transform.rotation, transform);
        ghostPlayer = GameObject.Instantiate(characterPrefab, position, transform.rotation, transform);
        _realPlayerSC = realPlayer.GetComponent<CharacterStateController>();
        _ghostPlayerSC = ghostPlayer.GetComponent<CharacterStateController>();
        _realPlayerSC.Init(this, CharacterTypes.human);
        _ghostPlayerSC.Init(this, CharacterTypes.ghost);

        realPlayer.layer = this.gameObject.layer;
        realPlayer.tag = "Human";
        realPlayer.GetComponentInChildren<SkinnedMeshRenderer>().material = humanMat;
        ghostPlayer.tag = "Ghost";
        ghostPlayer.GetComponentInChildren<SkinnedMeshRenderer>().material = humanMat;
        ghostPlayer.layer = this.gameObject.layer;

        switch(playerIndex)
        {
            case 0:
                realPlayer.GetComponentInChildren<Image>().color = new Color(0, 0.7f, 0.16f);
                ghostPlayer.GetComponentInChildren<Image>().color = new Color(0, 0.7f, 0.16f);
                break;
            case 1:
                realPlayer.GetComponentInChildren<Image>().color = new Color(0.3f, 0.26f, 1.0f);
                ghostPlayer.GetComponentInChildren<Image>().color = new Color(0.3f, 0.26f, 1.0f);
                break;
            case 2:
                realPlayer.GetComponentInChildren<Image>().color = new Color(0.65f, 0.1f, 0.1f);
                ghostPlayer.GetComponentInChildren<Image>().color = new Color(0.65f, 0.1f, 0.1f);
                break;
            case 3:
                realPlayer.GetComponentInChildren<Image>().color = new Color(0.8f, 0.7f, 0.05f);
                ghostPlayer.GetComponentInChildren<Image>().color = new Color(0.8f, 0.7f, 0.05f);
                break;
        } 
    }
}
