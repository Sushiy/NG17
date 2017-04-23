using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysics : MonoBehaviour {

    [SerializeField]
    float friction = 3f;

    [SerializeField]
    float maxVelocity = 50f;
    
    Vector3 targetDir;
    Vector3 targetDirMove;
    float step;
    float stepMove;
    new Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
    }
	
    void Update()
    {
        Vector3 frictionVelocity = Vector3.one * ( friction * Time.deltaTime);
        Vector3 velocity = rigidbody.velocity + targetDir * step + targetDirMove * stepMove;

        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);

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

        targetDir = Vector3.zero;
        targetDirMove = Vector3.zero;
        //lookTarget = Vector3.zero;
        step = 0;
        stepMove = 0;
    }

    public void LookWhereYoureGoing()
    {
        if (rigidbody.velocity.sqrMagnitude <= 0.1f)
            return;
        else
            transform.LookAt(transform.position + rigidbody.velocity * 100);
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
