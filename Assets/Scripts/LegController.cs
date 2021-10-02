using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LegPosition
{
    ForwardRight,
    ForwardLeft,
    RearRight,
    RearLeft,
}

public abstract class LegController : MonoBehaviour
{
    public KeyCode activationKey;
    public KeyCode negativeActivationKey;
    public LegPosition legPosition;

    protected bool activeLeg
    {
        get
        {
            return GoodBoy.instance.HasPower && (
                Input.GetKey(activationKey) || Input.GetKey(negativeActivationKey)
            );
        }
    }

    protected bool reverse
    {
        get
        {
            return Input.GetKey(negativeActivationKey);
        }
    }
    
}
