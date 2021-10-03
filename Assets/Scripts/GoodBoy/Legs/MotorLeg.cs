using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorLeg : LegController
{
    [SerializeField]
    bool canReverse;

    [SerializeField]
    float motorTargetForce = 400;

    [SerializeField]
    float motorForce = 500;
    
    HingeJoint joint;

    private void Awake()
    {
        joint = GetComponent<HingeJoint>();
        joint.enableCollision = false;
        var motor = joint.motor;
        motor.targetVelocity = motorTargetForce;
        motor.force = motorForce;
        joint.motor = motor;
    }

    public override void ConfigureJoint(Rigidbody connectedBody, Vector3 connectedPosition)
    {
        joint.connectedBody = connectedBody;
        joint.connectedAnchor = connectedPosition;
    }
    
    private void Update()
    {        
        joint.useMotor = activeLeg;
        if (joint.useMotor)
        {            
            var motor = joint.motor;
            motor.targetVelocity = reverse && canReverse ? -motorTargetForce : motorTargetForce;
            joint.motor = motor;
        }
    }
}
