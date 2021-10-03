using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputSettings
{
    public LegPosition legPosition;
    public string activationKeyDefault;
    private KeyCode activationKey = KeyCode.None;

    public string ActivationKeySetting {
        get
        {
            return string.Format("Leg.Activation.{0}", legPosition.ToString());
        }
    }

    public KeyCode ActiavtionKey
    {
        get
        {
            if (activationKey == KeyCode.None)
            {
                activationKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(ActivationKeySetting, activationKeyDefault));
            }
            return activationKey;
        }
    }

    public InputSettings(LegPosition leg, string activationKeyDefault)
    {
        legPosition = leg;
        this.activationKeyDefault = activationKeyDefault;
    }
}

public class GoodBoyInput
{
    public static bool HasPower;

    static Dictionary<LegPosition, InputSettings> defaltsKeys = new Dictionary<LegPosition, InputSettings>() {
        { LegPosition.ForwardLeft, new InputSettings(LegPosition.ForwardLeft, "A") },
        { LegPosition.ForwardRight, new InputSettings(LegPosition.ForwardRight, "J") },
        { LegPosition.RearLeft, new InputSettings(LegPosition.RearLeft, "S") },
        { LegPosition.RearRight, new InputSettings(LegPosition.RearRight, "K") },
    };

    public static bool IsActive(LegPosition leg)
    {
        return Input.GetKey(defaltsKeys[leg].ActiavtionKey);
    }

    const string REVERSE_KEY_SETTINGS = "Leg.Reverse";
    const string REVERSE_KEY_DEFAULT = "Space";
    static KeyCode reverseKey = KeyCode.None;

    public static bool InReverse
    {
        get
        {
            if (reverseKey == KeyCode.None)
            {
                reverseKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(REVERSE_KEY_SETTINGS, REVERSE_KEY_DEFAULT));
            }
            return Input.GetKey(reverseKey);
        }
    }
}
