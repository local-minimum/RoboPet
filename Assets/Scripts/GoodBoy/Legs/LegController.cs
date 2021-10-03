using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LegPosition
{
    ForwardLeft,
    ForwardRight,
    RearLeft,
    RearRight,
}

public abstract class LegController : MonoBehaviour
{
    public LegPosition legPosition;  
    public Vector3 anchorOffset;

    public abstract void ConfigureJoint(Rigidbody connectedBody, Vector3 connectedPosition);

    protected bool activeLeg
    {
        get
        {            
            return GoodBoy.instance.HasPower && GoodBoyInput.IsActive(legPosition);
        }
    }

    protected bool reverse
    {
        get
        {
            return GoodBoyInput.InReverse;
        }
    }
    
}
