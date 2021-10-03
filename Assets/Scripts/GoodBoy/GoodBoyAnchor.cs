using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BodyPart
{
    Body,
    Leg,
    Head
};

public abstract class GoodBoyAnchor : MonoBehaviour
{
    abstract public string SettingKey { get; }
    abstract public BodyPart BodyPart { get;  }
}
