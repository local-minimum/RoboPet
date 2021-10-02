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
    public LegPosition legPosition;
    protected KeyCode activationKey;
    private KeyCode reverseKey;
    public Vector3 anchorOffset;

    public void SetKeys(KeyCode activation, KeyCode reverse)
    {
        activationKey = activation;
        reverseKey = reverse;
    }

    protected bool activeLeg
    {
        get
        {            
            return GoodBoy.instance.HasPower && Input.GetKey(activationKey);
        }
    }

    protected bool reverse
    {
        get
        {
            return Input.GetKey(reverseKey);
        }
    }
    
}
