using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoLeg : LegController
{
    [SerializeField]
    float force;

    [SerializeField]
    float loadedPosition = -0.4f;

    [SerializeField]
    float cylceLength;

    [SerializeField]
    Transform piston;

    Rigidbody rb;

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
        rb = GetComponent<Rigidbody>();
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
            piston.localPosition = baseOffset * Mathf.Lerp(1, loadedPosition, t / cylceLength); 
            if (t > cylceLength)
            {
                t = 0;
                rb.AddForce(piston.up * force);
            }
        } else if (wasActive)
        {
            t = 0;            
            piston.localPosition = Vector3.Lerp(piston.localPosition, baseOffset, Time.deltaTime / cylceLength);
            if (Vector3.SqrMagnitude(baseOffset - piston.localPosition) < 0.001f)
            {
                wasActive = false;
            }
        }
    }
}
