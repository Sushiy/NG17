using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysics : MonoBehaviour {

    [SerializeField]
    float friction = 3f;

    [SerializeField]
    float maxVelocity = 50f;

    //[SerializeField]
    //float gravityScale = 0.5f;

    Vector3 targetDir;
    Vector3 targetDirMove;
    //Vector3 lookTarget;
    float step;
    float stepMove;
    new Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
    }
	
    void Update()
    {
        //Debug.Log("targetPos.x + targetPos.y + targetPos.z: "+(targetPos.x + targetPos.y + targetPos.z) + "; step: "+ step);

        //if (targetDir.x + targetDir.y + targetDir.z > 0.01f && step > 0.01f)
        
        //Debug.Log("transform.position: "+transform.position + " targetPos: " + targetDir + "; step: " + step + " targetPosMove: " + targetDirMove + "; stepMove: " + stepMove);
        
        //Vector3 posGravity = Vector3.MoveTowards(transform.position, transform.position + targetDir, step);

        //Vector3 posMove = Vector3.MoveTowards(posGravity, posGravity + targetDirMove, stepMove);

            //transform.position = posMove;// (posMove + posGravity)/ 2;
        //rigidbody.velocity = targetDir * gravityScale + targetDirMove;
            // rigidbody.velocity *= friction*Time.deltaTime;

        Vector3 frictionVelocity = Vector3.one * ( friction * Time.deltaTime);
        Vector3 velocity = rigidbody.velocity + targetDir * step + targetDirMove * stepMove;

        if (velocity.x > maxVelocity)
            velocity.x = maxVelocity;
        else if (velocity.x < -maxVelocity)
            velocity.x = -maxVelocity;

        if (velocity.y > maxVelocity)
            velocity.y = maxVelocity;
        else if (velocity.y < -maxVelocity)
            velocity.y = -maxVelocity;

        if (velocity.z > maxVelocity)
            velocity.z = maxVelocity;
        else if (velocity.z < -maxVelocity)
            velocity.z = -maxVelocity;


        //Vector3 velocity = targetDir * step + targetDirMove * stepMove;

        transform.LookAt(transform.position + targetDirMove * 100);

        if (velocity.x > frictionVelocity.x)
        {
            float temp = velocity.x;
            temp -= frictionVelocity.x;
            if (temp * velocity.x < 0)
                velocity.x = 0;
            else
                velocity.x = temp;
        } 
        else if (velocity.x < frictionVelocity.x)
        {
            float temp = velocity.x;
            temp += frictionVelocity.x;
            if (temp * velocity.x < 0)
                velocity.x = 0;
            else
                velocity.x = temp;
        }
        else
            velocity.x = 0;




        if (velocity.y > frictionVelocity.y)
        {
            float temp = velocity.y;
            temp -= frictionVelocity.y;
            if (temp * velocity.y < 0)
                velocity.y = 0;
            else
                velocity.y = temp;
        }
        else if (velocity.y < frictionVelocity.y)
        {
            float temp = velocity.y;
            temp += frictionVelocity.y;
            if (temp * velocity.y < 0)
                velocity.y = 0;
            else
                velocity.y = temp;
        }
        else
            velocity.y = 0;


        if (velocity.z > frictionVelocity.z)
        {
            float temp = velocity.z;
            temp -= frictionVelocity.z;
            if (temp * velocity.z < 0)
                velocity.z = 0;
            else
                velocity.z = temp;
        }
        else if (velocity.z < frictionVelocity.z)
        {
            float temp = velocity.z;
            temp += frictionVelocity.z;
            if (temp * velocity.z < 0)
                velocity.z = 0;
            else
                velocity.z = temp;
        }
        else
            velocity.z = 0;


        rigidbody.velocity = velocity;

        /*
        if (lookTarget.magnitude > 0.00f)
            transform.LookAt(targetDirMove * 100);
        
        */

        targetDir = Vector3.zero;
        targetDirMove = Vector3.zero;
        //lookTarget = Vector3.zero;
        step = 0;
        stepMove = 0;
    }





    public void accumulateLookTarget(Vector3 targetRot)
    {
        /*
        if(this.lookTarget.magnitude>0.01f)
            this.lookTarget = (this.lookTarget  + targetRot)/2;
        else
            */
            //this.lookTarget = targetRot;
    }

    public void accumulateTargetDir(Vector3 targetPos)
    {
        /*
        if (this.targetDir.magnitude > 0.01f)
            this.targetDir = (this.targetDir + targetPos) / 2;
        else
            */
            this.targetDir = targetPos;
    }

    public void accumulateSpeed(float step)
    {
            this.step = Mathf.Max(step, this.step);
    }



    public void accumulateTargetDirMove(Vector3 targetPos)
    {
        /*
        if (this.targetDirMove.magnitude > 0.01f)
            this.targetDirMove = (this.targetDirMove + targetPos) / 2;
        else
            */
            this.targetDirMove = targetPos;
    }

    public void accumulateSpeedMove(float step)
    {
        this.stepMove = Mathf.Max(step, this.stepMove);
    }

}
