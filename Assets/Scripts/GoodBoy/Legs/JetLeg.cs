using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetLeg : LegController
{
    [SerializeField]
    float force = 7;

    [SerializeField]
    Transform motor;

    [SerializeField]
    AnimationCurve forceEasing;

    [SerializeField]
    ParticleSystem ps;
    
    Rigidbody rb;

    bool wasActive = false;
    float burstStart;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void ConfigureJoint(Rigidbody connectedBody, Vector3 connectedPosition)
    {
        GetComponent<FixedJoint>().connectedBody = connectedBody;
    }

    private void Update()
    {
        if (activeLeg)
        {
            if (!wasActive)
            {
                burstStart = Time.timeSinceLevelLoad;
                wasActive = true;
                ps.Play();
            }            
            var factor = forceEasing.Evaluate(Time.timeSinceLevelLoad - burstStart);
            rb.AddForce(motor.up * force * factor);
        } else if (wasActive)
        {
            wasActive = false;
            ps.Stop();
        }        
    }
}
