using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LegController : MonoBehaviour
{
    public KeyCode activationKey;
    public KeyCode negativeActivationKey;

    protected bool activeLeg
    {
        get
        {
            return Input.GetKey(activationKey);
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
