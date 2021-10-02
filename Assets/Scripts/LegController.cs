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
