using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoLeg : LegController
{
    [SerializeField]
    AnimationCurve positioning;

    [SerializeField]
    float cylceLength;

    [SerializeField]
    Transform piston;

    public override void ConfigureJoint(Rigidbody connectedBody, Vector3 connectedPosition)
    {
        GetComponent<FixedJoint>().connectedBody = connectedBody;
    }

    Vector3 baseOffset;    
    bool wasActive = false;
    float t;

    private void Awake()
    {
        baseOffset = piston.localPosition;
    }

    private void Update()
    {
        if (activeLeg)
        {
            if (!wasActive) {
                t = 0;
                wasActive = true;
            }
            t += Time.deltaTime;
            piston.localPosition = baseOffset * positioning.Evaluate(Mathf.Min(t, cylceLength));

        } else if (wasActive)
        {
            t -= Time.deltaTime;
            if (t < 0)
            {
                wasActive = false;
                t = 0;
                piston.localPosition = baseOffset;
            } else
            {
                var off = piston.localPosition;
                piston.localPosition = Vector3.Lerp(off, baseOffset, t / cylceLength);
            }
        }
    }
}
