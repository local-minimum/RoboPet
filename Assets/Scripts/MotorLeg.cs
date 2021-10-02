using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorLeg : LegController
{
    float targetVelocity;
    HingeJoint joint;

    private void Start()
    {
        joint = GetComponent<HingeJoint>();
        targetVelocity = joint.motor.targetVelocity;
        joint.connectedBody = GoodBoy.instance.AnchorBody;
        joint.connectedAnchor = GoodBoy.instance.GetLegAnchor(legPosition);
    }

    private void Update()
    {        
        joint.useMotor = activeLeg;
        if (joint.useMotor)
        {
            var motor = joint.motor;
            motor.targetVelocity = reverse ? -targetVelocity : targetVelocity;
            joint.motor = motor;
        }
    }
}
